<?php
class dauser extends CI_Model {

	public function  BuscarIdUsuarioPorLoginSenha ($param){
		
		$senha =md5($param['senha']);
				
		$this->db->where ('email', $param['email']);
		$this->db->where ('senha',$senha );		
		$this->db->limit(1);
		
		$this->db->select('id,nome,email,nivel');
		
		$get = $this->db->get ('usuario');
		
		if ($get->num_rows >1){			
			return $get->result_array();				
		}else if($get->num_rows==1){			
			return $get->row_array ();
		}		
		return null;		
	}
	
	public function  ExisteLogin($email=null,$id=null){
		$this->db->limit (1);
		
		if($id!=null){
			$this->db->where ('id <>',$id);
		}
	
		$this->db->where ('email',$email);
		
		$get = $this->db->get ('usuario');	
		return ($get->num_rows);
	}
		
	public function  GerarSenha ($email){
		//senha sempre grava no banco em md5
		$senha = sha1($email);
		$senha = substr($senha, 0,5);

		$novaSenhaMd5= md5($senha);
		
		//gravar a senha no banco
		$this->db->where('email', $email);
		$this->db->set('senha', $novaSenhaMd5);		
		$this->db->update('usuario');
		
		return $senha;
	}
	
	public function Cadastrar($data) {	
		return $this->GetCadastrar('usuario',$data);	
	}
	
	public function Editar($data) {
	
		$this->db->where("id",$data['id']);
	
		if($this->db->update('usuario',$data)>0){
				
		}else{
			$this->db->trans_rollback();
			show_error ('Erro ao editar o usuario');
		}
	}
				
	public function ChecaSenha($id, $senha) {
					
		$this->db->where ('id',$id);
		$this->db->where ('senha',md5($senha) );
						
		$get = $this->db->get ('usuario');
				
		return $get->num_rows;		
	}
	//[fun��es genericas]
	// Obs->tabela de liga��o com o usuario tem que seguir o padr�o de nomeclatura no banco de dados
	public function VincularUsuarioNaTabela($tabela,$data) {
		return $this->GetCadastrar($tabela."_usuario", $data,'usuario_id');		
	}
	//[fun��es genericas]
	// Obs->tabela de liga��o com o usuario tem que seguir o padr�o de nomeclatura no banco de dados
	public function BuscarUsuarioDaTabela($tabela,$id){
		
		$this->db->where('eu.'.$tabela.'_id',$id);
		
		$this->db->join($tabela.'_usuario eu', 'u.id = eu.usuario_id');
		
		$this->db->select('u.id,u.nome,u.email,eu.ativo');
		
		$get = $this->db->get ('usuario u');
		
		return $this->GetRow($get);
	}

	public function  BuscarIdDonoDoUsuario($userId,$tabela){
		$tabelaNew=$tabela."_usuario u";
		$coluna= $tabela."_id";
				
		$this->db->where( 'u.usuario_id' ,$userId);		
		$this->db->select($coluna);		
		$get = $this->db->get ($tabelaNew);
		
		return $this->GetValue($get,$coluna);
	}

	public function BuscarSituacaoEstabelecimento($userId){
		
		$this->db->where( 'u.usuario_id' ,$userId);		
		$this->db->join( 'estabelecimento_usuario  u' ,'e.id = u.estabelecimento_id');		
		$this->db->select('e.situacao_id');						
		$get = $this->db->get ('estabelecimento e');		
		return $this->GetRow($get);
		
	}
	
	public function BuscarSituacaoCliente($userId){
	
		$this->db->where( 'u.usuario_id' ,$userId);
		$this->db->join( 'cliente_usuario u' ,'c.id = u.cliente_id');
		$this->db->select('c.situacao_id');	
		$get = $this->db->get ('cliente c');	
		return $this->GetRow($get);
	
	}

}