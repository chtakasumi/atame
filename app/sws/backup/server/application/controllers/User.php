<?php if ( ! defined('BASEPATH')) exit('No direct script access allowed');

class User extends CI_Controller {
	
	private function inicializarDA(){
		$this->load->model('DaMailConfig');			
	}

	public function BuscarUsuarioLogado() {	
		echo $this->getArrayToJson($this->sessaoBuscar());					
	}

	public function Deslogar() {
		print_r($this->sessaoDestruir());		
	}
		
	public function Logar() {
			
		$this->getPostArray();			
								
		$this->validarObjetoEmailSenha();
				
		$this->inicializarDA();	 
		
	    $array= $this->DaUser->BuscarIdUsuarioPorLoginSenha($this->entidade);
	   
	    
	    if($this->isNullOrEmpty($array)){
	    	$this->msgAdvertencia('Login ou senha incorreto.');
	    }
	    	    
	    $ativo =  $this->DaUser->BuscarSituacaoEstabelecimento($array['id']);	    	    
	    if($ativo==null){
	    	$ativo =  $this->DaUser->BuscarSituacaoCliente($array['id']);
	    }	    
	    if($ativo==null){
	    	$ativo['situacao_id'] =1;
	    }	    
	   	    
	    $novoArray = array(
	    		'id'=>$array['id'],
	    		'nome'=>$array['email'],
	    		'email'=>$array['nome'],
	    		'nivel'=>$array['nivel'],
	    		'ativo'	=>$ativo['situacao_id']);
	    		
	    		
	    	
	    		
	    
	   $this->sessaoCriar($novoArray);
	   	 	    
	}
		
	public function Cadastrar() {
		$this->getPostArray();	
				
		$this->inicializarDA();	
		$this->DaUser->TransacaoAbrir();		
		
		$tabUsuario =$this->entidade['Entidade'];
		
		//validar campos
		$array = array(			
				'email'=>$tabUsuario['email'],
				'nome'=>$tabUsuario['nome'],
				'nivel'=>$tabUsuario['nivel'],
				'senha'	=>$tabUsuario['senha']
		);			
		$camposObrigatorios = array(
				'nome'=>'Nome',
				'email'=>'E-mail',
				'nivel'=>'Erro de Regarregamento da Pagina. Precione CTRL+F5 e cadastre novamente.',
				'senha'	=>'Senha'
		);
		$this->ValidarCampos($array, $camposObrigatorios);		
		$this->ExisteLogin($tabUsuario['email']);
			
		$tabelaParaVincular =$this->entidade['TabelaParaVincular'];
		
		//cadastra na tabela usuario
		$tabUsuario['token']=md5(sha1($tabUsuario['email']));
		$tabUsuario['senha']=md5($tabUsuario['senha']);	
				
		
		$entidade = $this->DaUser->Cadastrar($tabUsuario);	
		
		//Vincula usuario a outra  tabela	
		//monta o novo objeto	
		$UsuarioVincular = array(
				'usuario_id'=> $entidade['id'],
				$tabelaParaVincular['tabela_nome'].'_id'=>$tabelaParaVincular['tabela_id'],				
				'ativo'	=>$tabelaParaVincular['ativo']
		);
					
		 //estabelecimento_usuario		
		$this->DaUser->VincularUsuarioNaTabela($tabelaParaVincular['tabela_nome'],$UsuarioVincular);	
	
		$this->DaUser->TransacaoConfirmar();		
		
		echo $this->msgSucesso("Usuário Cadastrado com sucesso");
				
	}
	
	//editar
	public function Editar() {
		$this->getPostArray();
	
		$this->inicializarDA();
		
		if($this->isNullOrEmpty($this->entidade['senha'])){
			
			$novoUsuario = array(
					'id'=>$this->entidade['id'],
					'email'=>$this->entidade['email'],
					'nome'=>$this->entidade['nome'],
					'nivel'=>$this->entidade['nivel'],
					'token'	=>md5(sha1($this->entidade['email']))				
			);		
			
			$camposObrigatorios = array(
					'nome'=>'Nome',
					'email'=>'E-mail',	
			);
			
			
		}else{
			
			$novoUsuario = array(
					'id'=>   $this->entidade['id'],
					'email'=>$this->entidade['email'],
					'nome'=>$this->entidade['nome'],
					'nivel'=>$this->entidade['nivel'],
					'token'	=>md5(sha1($this->entidade['email'])),
					'senha'	=>md5($this->entidade['senha'])
			);
			
			$camposObrigatorios = array(
					'nome'=>'Nome',
					'email'=>'E-mail',					
					'senha'	=>'Senha'
			);
		}
		
		//validar campos		
		$this->ValidarCampos($novoUsuario, $camposObrigatorios);

		$this->ExisteLogin($this->entidade['email'],$this->entidade['id']);
				
		$this->DaUser->Editar($novoUsuario);
		
		echo $this->msgSucesso("Usuário editado com sucesso");
	
	}
	
	public function RecuperarSenha() {
			
		$this->inicializarDA();		
		
		$array = $this->DaMailConfig->GetConfiguracao();
		
		$this->getPostArray();		
		
		$titulo=$array['empresa']." - Recuperação de senha";
		$mensagem="Sua nova senha é: " . $this->DaUser->GerarSenha($this->entidade['email']);
	
		$this->DaMailConfig->Send($this->entidade['email'],$titulo,$mensagem);

		echo $this->msgSucesso("Sua senha foi enviada para sua caixa de email. você será redirecionado...");		
	}

	public function BuscarUsuarioDaTabela(){
		
		$this->inicializarDA();
		
		$this->getPostArray();
				
		$id =$this->entidade['id'];
		$tabela =$this->entidade['tabela'];	
		
		$jsonUsuario = $this->getArrayToJson ($this->DaUser->BuscarUsuarioDaTabela($tabela,$id));		
		
		echo $this->MontarObjetoRetornoJson ($jsonUsuario);
	}	

	public function ChecaSenha(){
		
		$this->inicializarDA();
		
		$this->getPostArray();
		
		echo  $this->DaUser->ChecaSenha($this->entidade['usuario_id'],$this->entidade['senha1']);
		
	} 
	
	//Validações
	private function validarObjetoEmailSenha(){
				
		if($this->isNullOrEmpty($this->entidade['email'])){
			$this->msgAdvertencia("Preencha o campo email");
		}
		
		if($this->isNullOrEmpty($this->entidade['senha'])){
			$this->msgAdvertencia("Preencha o campo senha");
		}		
	}
	
	private function validarDadosObrigatorios(){
		
		if($this->isNullOrEmpty($this->entidade['nome'])){
			$this->msgAdvertencia("Preencha o campo nome");
			return;
		}
		
		if($this->isNullOrEmpty($this->entidade['email'])){
			$this->msgAdvertencia("Preencha o campo email");
			return;
		}		
	}
	
	private function ExisteLogin($email=null,$id=null){
		if($this->DaUser->ExisteLogin($email,$id)){
			$this->msgAdvertencia("Este email já existente. Use outro email.");
		}
	}
		
	//teste
	public function Sessao() {
		///teste
		print_r($this->sessaoBuscar());
	}
	
	//teste
	public function DestroiSessao() {
		///teste
		$this->sessaoDestruir();
		echo "sessao destruida";
	}
	
	//teste
	public function Md5($senha) {
		///teste
		print_r(md5($senha));
	}
}