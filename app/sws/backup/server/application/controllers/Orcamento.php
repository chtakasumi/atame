<?php
if (! defined ( 'BASEPATH' ))
	exit ( 'No direct script access allowed' );
class Orcamento extends CI_Controller {
	
	private function InicializarDA() {
		$this->load->model ( 'DaOrcamento' );
		$this->load->model ( 'DaPedido' );
		$this->load->model ( 'DaUser' );
	}	

	private function CarregaLovs(){
		$this->load->model ( 'DaSituacao' );
		$this->load->model ( 'DaEstabelecimento' );		
		
		$jsonSituacao = $this->getArrayToJson ( $this->DaSituacao->Buscar ( 3 ) );		
		$jsonEstabelecimento = $this->getArrayToJson ( $this->DaEstabelecimento->BuscarPorId ($this->entidade['estabelecimento_id'],"id, nome_fantasia texto") );		
		$jsonPedido = $this->getArrayToJson ( $this->DaPedido->BuscarPorId ($this->entidade['pedido_id'],"observacao,situacao_id") );
		$jsonItemOrcamento = $this->getArrayToJson ( $this->DaOrcamento->BuscarItemOrcamento ($this->entidade['pedido_id'],$this->entidade['id']) );
				
		return '{
					"situacao":' . $jsonSituacao . ',	
					"estabelecimento":' . $jsonEstabelecimento . ',
					"pedido":' . $jsonPedido . ',
					"item":' . $jsonItemOrcamento . '
			    }';
	}
	
	public function Buscar(){
		$this->getPostArray();			
		$this->InicializarDA ();
		
		$id_pedido=$this->entidade['pedido_id'];
		$id=$this->entidade['id'];
	
		//se me traz o ultimo pedido do usuario dono da sessao caso houver
		$codigo = $this->BuscarIdDonoDoUsuario();
		$sessao = $this->sessaoBuscar();
				
		if($sessao['nivel'] == 3){
			$this->entidade = $this->DaOrcamento->BuscarPedidoDoEstabelecimento($codigo,$id_pedido);			
		}
		if($this->isNullOrEmpty($this->entidade['id'])){
			$this->entidade = $this->DaOrcamento->BuscarPorId ($id);
		}
		
		if($this->isNullOrEmpty($this->entidade['id'])){
			$this->entidade['pedido_id']=$id_pedido;
			$this->entidade['id']=$id;
		}	

		
		$lovs = $this->CarregaLovs ();		
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ($this->entidade), $lovs );
	}
	
	public function Cadastrar(){
		$this->getPostArray ();
		$this->Validacao ();
		$this->InicializarDA ();
	
		$this->DaOrcamento->TransacaoAbrir ();
	
		$dados = $this->entidade ['dados'];
		$item = $this->entidade ['item'];
	
		$sessao = $this->sessaoBuscar ();
	
		if ($sessao ['nivel'] == 1 && ! $dados ['estabelecimento_id'] > 0) {
			echo $this->msgAdvertencia ( "Este usuário é uma adminstrador selecione um estabelecimento..." );
			return;
		}
		if (! $dados ['estabelecimento_id'] > 0) {
			$estabelecimentoId = $this->DaUser->BuscarIdDonoDoUsuario ( $sessao ['id'], 'estabelecimento' );
			$dados ['estabelecimento_id'] = $estabelecimentoId;
		}
	
		$retorno = $this->DaOrcamento->Cadastrar ( $dados );
		
		$retorno ['estabelecimento_id'] = $dados ['estabelecimento_id'];
	
		$this->DaOrcamento->CadastrarItem ( $retorno ['id'], $item );
	
		$this->DaOrcamento->TransacaoConfirmar ();
	
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $retorno ), null, $this->msgSucesso ( "Orçamento cadastrado com sucesso." ) );
	}
	
	public function Editar(){
		$this->getPostArray ();
		$this->Validacao ();
		$this->InicializarDA ();
	
		$this->DaOrcamento->TransacaoAbrir ();
	
		$dados = $this->entidade ['dados'];
		$item = $this->entidade ['item'];
	
		$sessao = $this->sessaoBuscar ();
	
		if ($sessao ['nivel'] == 1 && ! $dados ['estabelecimento_id'] > 0) {
			echo $this->msgAdvertencia ( "Este usuário é uma adminstrador. Selecione um estabelecimento..." );
			return;
		}
	
		if (! $dados ['estabelecimento_id'] > 0) {
			$estabelecimentoId = $this->DaUser->BuscarIdDonoDoUsuario ( $sessao ['id'], 'estabelecimento' );
			$dados ['estabelecimento_id'] = $estabelecimentoId;
		}
	
		$this->DaOrcamento->Editar ($dados);
		$this->DaOrcamento->EditarItem ($item);
			
		$this->DaOrcamento->TransacaoConfirmar ();
	
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $dados), null, $this->msgSucesso ( "Orçamento editados com sucesso." ) );
	}
	
	public function AlterarSituacao() {
		// enviar
		$this->initAlterarSituacao();
		$lovs = $this->CarregaLovs ();
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $this->entidade ), $lovs, $this->msgAviso ( "Seu orçamento foi envido ao cliente. Você não poderá mas alterar o orçamento, apenas cancelar." ) );
	}
			
	public function AlterarSituacaoAprovar() {
		// aprovar
		$this->initAlterarSituacao();
		$retorno = $this->initListar();		
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ($retorno), null, $this->msgSucesso( "Orçamento foi aprovado." ) );
	}
	
	public function Cancelar() {
		$this->getPostArray ();
		$this->InicializarDA ();
		$this->DaOrcamento->AlterarSituacao($this->entidade['id'],$this->entidade['situacao_id']);
		//contar quantos orçamentos tem no pedido, se for igual a zero mudar situacao do pedido para 3	Enviado/Em Aberto.
		if($this->DaOrcamento->ContarOrcamentoEmAnalise($this->entidade['pedido_id'])===0){
			$this->DaPedido->AlterarSituacao( $this->entidade['pedido_id'],3);
		}
		$lovs = $this->CarregaLovs ( $this->entidade ['id'] );
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $this->entidade ), $lovs, $this->msgAviso ( "Seu Orçamento foi cancelado. Não poderá ser mais editado." ) );
	}
	
	public function CancelarPelaListagem(){
		$this->getPostArray ();
		$this->InicializarDA ();
		$this->DaOrcamento->AlterarSituacao($this->entidade ['id'], $this->entidade ['situacao_id']);
		
		if($this->DaOrcamento->ContarOrcamentoEmAnalise($this->entidade['pedido_id'])===0){
			$this->DaPedido->AlterarSituacao( $this->entidade['pedido_id'],3);
		}
				
		$this->entidade = $this->initListar ($this->entidade ['id']);
		echo $this->MontarObjetoRetornoJson ($this->getArrayToJson ( $this->entidade ), null, $this->msgAviso ( "Seu orçamento foi cancelado." ) );		
	}
	
	public function EnviarPelaListagem(){
		$this->initAlterarSituacao();
		$this->entidade = $this->initListar ($this->entidade ['id']);
		echo $this->MontarObjetoRetornoJson ($this->getArrayToJson ( $this->entidade ), null, $this->msgAviso (  "Seu orçamento foi envido ao cliente. Você não poderá mas alterar o orçamento, apenas cancelar." ) );
	}	
	
	public function Listar() {
		$retorno =$this->initListar();
		echo $this->getArrayToJson ( $retorno );
	}
		
	private function initAlterarSituacao(){
		//usando em enviar e aprovar.		
		$this->getPostArray ();
		$this->InicializarDA ();
		
		$this->DaOrcamento->AlterarSituacao( $this->entidade['id'],$this->entidade['situacao_id']);			
				
		//Se o pedido estiver na situação 3 - Enviado/Em Aberto.
		$total =$this->DaPedido->ContarPedidoEnviado($this->entidade['pedido_id']);
		if($total>0){
			$this->DaPedido->AlterarSituacao( $this->entidade['pedido_id'],$this->entidade['situacao_id']);
		}
						
		if($this->entidade['estabelecimento_id']!=null && $this->entidade['situacao_id']==5){
			$this->DaPedido->AlterarSituacao($this->entidade['pedido_id'],$this->entidade['situacao_id'],$this->entidade['estabelecimento_id']);
		}
	}	
		
	private function initListar($id=null){
		$this->inicializarDA ();
		$this->getPostArray ();
					
		$sessao = $this->sessaoBuscar ();
		$estabelecimentoId=null;
		
		if($sessao ['nivel'] == 3){			
			$estabelecimentoId = $this->BuscarIdDonoDoUsuario();
		}			
		
		return $this->DaOrcamento->Listar ($this->entidade,$id, $estabelecimentoId);		
	}
		
	private function Validacao() {
		$dados = $this->entidade ['dados'];
		$item = $this->entidade ['item'];
				
		$camposObrigatorios = array (
				//'situacao_id' => 'Situação não econtrada.  Informe ao suporte sobre esta mensagem',
				//'pedido_id' => 'Pedido não encontrado. Informe ao suporte sobre esta mensagem',
				//'valor_total' => 'Valor Total. Informe ao suporte sobre esta mensagem',
				//'sub_total' => 'Sub Total. Informe ao suporte sobre esta mensagem',
				'prazo_entrega' => 'Prazo entrega',
		);
		
		$this->ValidarCampos ($dados, $camposObrigatorios );
				
		if($dados['desconto']>$dados['valor_total']){
			$this->msgAdvertencia('Desconto não pode ser maior que valor total.');
		}
		
		if ($this->isNullOrEmpty ( $this->entidade ['item'] )) {
			$this->msgAdvertencia ( "Insira ao menos 1 item." );
		}
		
		foreach ($item as $key => $value){
			if( $this->isNullOrEmpty($value['preco_unitario'])){				
				$this->msgAdvertencia ( "Informe o preço do item ". $value['descricao'] );
				break;
			}else if($value['preco_unitario'] <= 0 ){
				$this->msgAdvertencia ( "Informe um preço superior a 0 no item ". $value['descricao'] );
				break;
			}		
		}
	}
}