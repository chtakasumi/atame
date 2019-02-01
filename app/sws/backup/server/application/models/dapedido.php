<?php
class DaPedido extends CI_Model {
	
	public function BuscarItem($id) {
		$this->db->where ( 'pedido_id', $id );
		$this->db->select ( '*' );
		$get = $this->db->get ( 'item' );
		
		return $this->GetRow ( $get );
	}
	
	public function BuscarPorId($id,$colunas='*') {
		$this->db->where ('id',$id);
		$this->db->select ($colunas);
		$get = $this->db->get ( 'pedido' );
		
		return $this->GetRow ( $get, 'pedido',$colunas);
	}
	
	public function ContarPedidoEnviado($id) {
		$this->db->where ('id',$id);
		$this->db->where ('situacao_id',3);
		$this->db->select ('id');
		$get = $this->db->get ( 'pedido' );	
		return $get->num_rows;
	}
		
	public function Cadastrar($data) {
		return $this->GetCadastrar ( 'pedido', $data );
	}
	
	public function Editar($data) {
		$notificacao = $this->Notificacao ();	
		$data = date("Y-m-d H:i:s");
		
		$this->db->where("id",$data['id']);		
		$this->db->set('notificacao',$notificacao);	
		$this->db->set('data_notificacao',$data);
		
		if(!$this->db->update('pedido',$data)>0){
			$this->db->trans_rollback();
			show_error ('Erro ao editar o Pedido');
		}			
	}
	
	public function AlterarSituacao($id,$novaSituacao,$estabelecimentoId=null) {

		$notificacao = $this->Notificacao ();
		
		$this->db->where("id",$id);		
		$this->db->set('situacao_id',$novaSituacao);
		$this->db->set('notificacao',$notificacao);		
		
		if($estabelecimentoId!=null){
			$data = date("Y-m-d H:i:s");			
			$this->db->set('estabelecimento_id',$estabelecimentoId);
			$this->db->set('data_aprovacao',$data);
			$this->db->set('data_notificacao',$data);
		}
				
		if(!$this->db->update('pedido')>0){
			$this->db->trans_rollback();
			show_error ('Erro ao alterar situacao  do pedido');
		}
		
	}	
		
	public function CadastrarItem($id,$array) {
		foreach($array as $key => $item) {	
			
			$retorno =false;					
			$dados = array (
					'descricao' => $item ['descricao'],
					'quantidade' => $item ['quantidade'],
					'pedido_id' => $id
			);	
						
			$this->GetCadastrar('item', $dados);
			$retorno = true;
		}

		return $retorno;
	}
	
	public function ExcluirItem($id) {
		$this->db->where ('pedido_id', $id );
		$this->db->delete ( 'item' );		
	}

	public function Listar($filtro=null, $codigo=null,$id=null,$nivel=null,$estalecimentoSessao=null) {
			
		$this->db->limit(100);		
		$this->db->order_by('p.id','desc');		
	
		if($nivel==2){			
			$this->db->having('p.cliente_id',$codigo);		
		}				
		if($nivel==3){			
			$this->db->having('p.categoria_id',$codigo);
			$this->db->having('p.situacao_id in (3,5,6)',null,FALSE);			
		}		
		if($id!=null){
			$this->db->where('p.id',$id);	 
		}		
		if($filtro!=null){
			$this->db->or_like('p.id',$filtro);			
			$this->db->or_like('p.observacao', $filtro);
			$this->db->or_like('c.descricao', $filtro);
			$this->db->or_like('s.descricao',$filtro);
			$this->db->or_like('t.nome_fantasia',$filtro);
			$this->db->or_like('date_format( p.data_criacao, \'%d/%m/%Y \')',$filtro);
		}
	
		$this->db->join('situacao s','p.situacao_id=s.id');
		$this->db->join('categoria c','p.categoria_id=c.id');	
		$this->db->join('cliente t','p.cliente_id=t.id');
				
		$this->db->select ("p.id,
				p.observacao,
				p.cliente_id,				
				t.nome_fantasia cliente,
				p.categoria_id,
				p.situacao_id,
			   	(case when p.estabelecimento_id =" .$estalecimentoSessao." then true else false end ) meu_estabelecimento,
				s.descricao situacao_descricao,
				c.descricao categoria_descricao,	
				(select count(id) from orcamento o where o.pedido_id=p.id and o.situacao_id in(5,6,7) ) qtd_orcamento,
				(select count(id) from orcamento o where o.pedido_id=p.id and o.estabelecimento_id=".$estalecimentoSessao.") qtd_orcamento_estabelecimento,			
				s.id situacao_id,date_format(p.data_criacao,'%d/%m/%Y') as data_criacao",FALSE);
					
		$get = $this->db->get ('pedido p');
	
		return $this->GetRow($get);	
	}	
	
	private function Notificacao (){
		$this->db->select_max ('notificacao');
		$get = $this->db->get ('pedido');	
		$notificacao = $get->row_array ();
		return  $notificacao['notificacao'] + 1;
	}
	
	public function ListarCincoUltimosPedidos($nivel=null,$codigo=null){
		
		$this->db->limit(5);
		$this->db->order_by('p.notificacao','desc');
				
		if($nivel==2){
			$this->db->where('p.cliente_id',$codigo);
		}
		if($nivel==3){
			$this->db->where('p.categoria_id',$codigo);			
		}
						
		$this->db->where_in('p.situacao_id',array(3,4,5,6,7));		
		
		$this->db->join('situacao s','p.situacao_id=s.id');
		$this->db->join('categoria c','p.categoria_id=c.id');
		$this->db->join('cliente t','p.cliente_id=t.id');
		$this->db->join('estabelecimento e ','p.estabelecimento_id=e.id','LEFT');	
		
		$this->db->select ("p.id,
				t.nome_fantasia cliente,
				s.descricao situacao_descricao,
				p.situacao_id,					
				date_format(p.data_notificacao,'%d/%m/%Y') data_notificacao,
				e.nome_fantasia estabelecimento,							
				c.descricao categoria_descricao",FALSE);
		
		$get = $this->db->get ('pedido p');
		
		return $this->GetRow($get);
	}	

}