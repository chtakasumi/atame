<?php
if (! defined ( 'BASEPATH' ))
	exit ( 'No direct script access allowed' );
class Pedido extends CI_Controller {
	
	private function InicializarDA() {
		$this->load->model ( 'DaPedido' );	
		$this->load->model ( 'DaOrcamento' );
		$this->load->model ( 'DaUser' );
	}
	
	private function CarregaLovs($id,$cliente_id=0) {
		$this->load->model ( 'DaSituacao' );
		$this->load->model ( 'DaCategoria' );
		$this->load->model ( 'DaCliente' );
		
		$jsonSituacao = $this->getArrayToJson ( $this->DaSituacao->Buscar ( 3 ) );
		$jsonCategoria = $this->getArrayToJson ( $this->DaCategoria->Buscar () );
		$jsonItemPedido = $this->getArrayToJson ( $this->DaPedido->BuscarItem ( $id ) );
		$jsonCliente = $this->getArrayToJson ( $this->DaCliente->BuscarPorId ($cliente_id, 'id,nome_fantasia texto') );
		
		return '{
					"situacao":' . $jsonSituacao . ',					
					"categoria":' . $jsonCategoria . ',
					"cliente":' . $jsonCliente . ',
					"item":' . $jsonItemPedido . '
			    }';
	}
	
	public function Buscar($id = 0) {
		$this->InicializarDA ();					
		$this->entidade = $this->DaPedido->BuscarPorId ( $id );		
		$lovs = $this->CarregaLovs ($id,$this->entidade['cliente_id']);		
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $this->entidade ), $lovs );
	}
	
	public function Cadastrar() {
		$this->getPostArray ();
		$this->Validacao ();
		$this->InicializarDA ();
		
		$this->DaPedido->TransacaoAbrir ();
		
		$dados = $this->entidade ['dados'];
		$item = $this->entidade ['item'];
		
		$sessao = $this->sessaoBuscar ();
		
		if ($sessao ['nivel'] == 1 && ! $dados ['cliente_id'] > 0) {
			echo $this->msgAdvertencia ( "Este usuário é uma adminstrador selecione um cliente..." );
			return;
		}
		
		if (! $dados ['cliente_id'] > 0) {
			$clienteId = $this->DaUser->BuscarIdDonoDoUsuario ( $sessao ['id'], 'cliente' );
			$dados ['cliente_id'] = $clienteId;
		}
		
		$retorno = $this->DaPedido->Cadastrar ( $dados );
		$retorno ['cliente_id'] = $dados ['cliente_id'];
		
		$this->DaPedido->CadastrarItem ( $retorno ['id'], $item );
		
		$this->DaPedido->TransacaoConfirmar ();
		
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $retorno ), null, $this->msgSucesso ( "Pedidos cadastrado com sucesso." ) );
	}
	
	public function Editar() {
		$this->getPostArray ();
		$this->Validacao ();
		$this->InicializarDA ();
		
		$this->DaPedido->TransacaoAbrir ();
		
		$dados = $this->entidade ['dados'];
		$item = $this->entidade ['item'];
		
		$sessao = $this->sessaoBuscar ();
		
		if ($sessao ['nivel'] == 1 && ! $dados ['cliente_id'] > 0) {
			echo $this->msgAdvertencia ( "Este usuário é uma adminstrador. Selecione um cliente..." );
			return;
		}
		
		if (! $dados ['cliente_id'] > 0) {
			$clienteId = $this->DaUser->BuscarIdDonoDoUsuario ( $sessao ['id'], 'cliente' );
			$dados ['cliente_id'] = $clienteId;
		}
		
		$this->DaPedido->Editar ( $dados );
		$this->DaPedido->ExcluirItem ( $dados ['id'] );
		$this->DaPedido->CadastrarItem ( $dados ['id'], $item );
		
		$this->DaPedido->TransacaoConfirmar ();
		
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $dados ), null, $this->msgSucesso ( "Pedidos editados com sucesso." ) );
	}
	
	// vou deprecisar esse metodo
	public function MudarSituacaoPedido() {
		//enviar
		$this->getPostArray ();
		$this->InicializarDA ();	
		$this->DaPedido->AlterarSituacao ($this->entidade ['id'], $this->entidade ['situacao_id'] );
		$lovs = $this->CarregaLovs ( $this->entidade ['id'] );
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $this->entidade ), $lovs, $this->msgAviso ( "Seu pedido foi envido a todos os fornecedores. Você não poderá mas alterar o pedido, apenas cancelar." ) );
	}
	
	// vou deprecisar esse metodo
	public function Cancelar() {
		$this->getPostArray ();
		$this->InicializarDA ();
		$this->DaPedido->AlterarSituacao ($this->entidade ['id'], $this->entidade ['situacao_id'] );
		$this->DaOrcamento->AlterarSituacaoPeloPedido($this->entidade ['id'], $this->entidade ['situacao_id'] );
		
		$lovs = $this->CarregaLovs ( $this->entidade ['id'] );
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $this->entidade ), $lovs, $this->msgAviso ( "Seu pedido foi cancelado. Não poderá ser mais editado." ) );
	}
	
	// ***Para Alterar Situações
	public function AlterarSituacao() {
		$this->getPostArray ();
		$this->InicializarDA ();
		$this->DaPedido->AlterarSituacao ($this->entidade ['id'], $this->entidade ['situacao_id'] );		
		$this->DaOrcamento->AlterarSituacaoPeloPedido($this->entidade ['id'], $this->entidade ['situacao_id'] );
								
		$msg = $this->CriarMensagem($this->entidade ['situacao_id']);
		$this->entidade = $this->initListar (null,$this->entidade ['id']);				
		echo $this->MontarObjetoRetornoJson ( $this->getArrayToJson ( $this->entidade ), null, $this->msgAviso ( $msg ) );
	}
		
	public function Listar() {
		$this->inicializarDA ();
		$this->getPostArray ();	
		
		if(!$this->isNullOrEmpty($this->entidade['pedido_id'])){			
			$retorno = $this->initListar (null,$this->entidade['pedido_id']);
		}else{
			$retorno = $this->initListar ($this->entidade);
		}	
							
		echo $this->getArrayToJson ($retorno );
		
	}
	
	private function InitListar($filtro=null,$id = null) {
		
		$sessao = $this->sessaoBuscar ();				
		$codigo = $this->BuscarIdDonoDoUsuario();
		$estalecimentoSessao = 0;
				
		if($sessao ['nivel'] == 3){	
			$this->load->model('DaEstabelecimento');
			$codigo = $this->DaEstabelecimento->BuscarPorId($codigo,'categoria_id');
			$codigo = $codigo['categoria_id'];
			$estalecimentoSessao = $this->BuscarIdDonoDoUsuario();
		}
						
		return  $this->DaPedido->Listar ($filtro, $codigo,$id,$sessao ['nivel'],$estalecimentoSessao);
	}
	
	private function CriarMensagem($situacao) {
		switch ($situacao) {
			case 3 : // Enviado
				return "Seu pedido foi envido a todos os fornecedores. Você não poderá mas alterar o pedido, apenas cancelar.";
				break;
			case 4 : // Cancelar
				return "Seu pedido foi cancelado. Não poderá ser mais editado.";
				break;
		}
	}
	
	private function Validacao() {
		$camposObrigatorios = array (
				'categoria_id' => 'Categoria',
				'observacao' => 'Descrição',
				'situacao_id' => 'Situacao' 
		);
		$this->ValidarCampos ( $this->entidade ['dados'], $camposObrigatorios );
		
		if ($this->isNullOrEmpty ( $this->entidade ['item'] )) {
			$this->msgAdvertencia ( "Insira ao menos 1 item." );
		}
	}
}