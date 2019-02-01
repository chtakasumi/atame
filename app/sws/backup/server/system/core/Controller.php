<?php
if (! defined ( 'BASEPATH' ))
	exit ( 'No direct script access allowed' );
/**
 * CodeIgniter
 *
 * An open source application development framework for PHP 5.1.6 or newer
 *
 * @package CodeIgniter
 * @author ExpressionEngine Dev Team
 * @copyright Copyright (c) 2008 - 2014, EllisLab, Inc.
 * @license http://codeigniter.com/user_guide/license.html
 * @link http://codeigniter.com
 * @since Version 1.0
 * @filesource
 *
 */
	
// ------------------------------------------------------------------------

/**
 * CodeIgniter Application Controller Class
 *
 * This class object is the super class that every library in
 * CodeIgniter will be assigned to.
 *
 * @package CodeIgniter
 * @subpackage Libraries
 * @category Libraries
 * @author ExpressionEngine Dev Team
 * @link http://codeigniter.com/user_guide/general/controllers.html
 */
class CI_Controller {
	private static $instance;
	public $entidade;
	public function __construct() {
		self::$instance = & $this;
		
		// Assign all the class objects that were instantiated by the
		// bootstrap file (CodeIgniter.php) to local class variables
		// so that CI can run as one big super object.
		foreach ( is_loaded () as $var => $class ) {
			$this->$var = & load_class ( $class );
		}
		
		$this->load = & load_class ( 'Loader', 'core' );
		$this->load->library ( 'session' );
		$this->load->model ( 'DaUser' );		
		
		/*
		 * if($this->isNullOrEmpty($this->sessaoBuscar())){
		 * $this->msgErro("Usuário não autenticado.");
		 * return false;
		 * }
		 */
		
		$this->load->initialize ();
	}
	public static function &get_instance() {
		return self::$instance;
	}
	
	// *****Obter dados via Post******
	protected function getPostJson() {
		$this->entidade = file_get_contents ( 'php://input' );
		return $this->entidade;
	}
	protected function getPostArray() {
		$this->entidade = json_decode ( file_get_contents ( 'php://input' ), true );
		return $this->entidade;
	}
	protected function MontarObjetoRetornoJson($entidade = null, $lovs = null, $mensagem = null) {
		if ($entidade == null) {
			$entidade = 'null';
		}
		if ($mensagem == null) {
			$mensagem = 'null';
		}
		if ($lovs == null) {
			$lovs = 'null';
		}
		
		return '{"Entidade":' . $entidade . ', "Lov":' . $lovs . ', "Mensagem":' . $mensagem . ' }';
	}
	
	// *****converter******
	protected function getArrayToJson($array) {
		return json_encode ( $array );
	}
	protected function getJsonToArray($stringJson) {
		return json_decode ( $stringJson, true );
	}
	
	// *****Mensagens do servidor******
	protected function msgSucesso($mensagem) {
		return '[{"Mensagem": "' . htmlentities ( $mensagem ) . '","Tipo":"1"}]';
	}
	protected function msgAviso($mensagem) {
		return '[{"Mensagem": "' . htmlentities ( $mensagem ) . '","Tipo":"2"}]';
	}
	protected function msgAdvertencia($mensagem) {
		die ( '[{"Mensagem":"' . htmlentities ( $mensagem ) . '","Tipo":"3"}]' );
	}
	protected function msgErro($mensagem) {
		die ( '[{"Mensagem": "' . htmlentities ( $mensagem ) . '","Tipo":"4"}]' );
	}
	
	// *****Validacao******
	protected function isNullOrEmpty($valor) {
		return (! isset ( $valor ) || empty ( $valor ));
	}
	protected function ValidarCampos($array, $camposObrigatorios) {
		if ($this->isNullOrEmpty ( $array )) {
			echo $this->msgAdvertencia ( "Preencha os campos obrigatórios" );
			return;
		}
		
		do {
			$alias = current ( $camposObrigatorios );
			$coluna = key ( $camposObrigatorios );
			$valor = $array [$coluna];
			
			if ($this->isNullOrEmpty ( $valor )) {
				echo $this->msgAdvertencia ( "Preencha o campo " . $alias );
				return;
			}
		} while ( next ( $camposObrigatorios ) );
	}
	
	// *****sessão******
	protected function sessaoCriar($user) {
		$this->session->set_userdata ( 'user', $user );
	}
	protected function sessaoBuscar() {
		return $this->session->userdata ( 'user' );
	}
	protected function sessaoDestruir() {
		$this->session->unset_userdata ( 'user' );
	}
	//**************data e hora****************
	
	protected function getData() {
		return date("d/m/Y");
	}
		
	protected function getDataHora() {
		return date("d/m/Y H:i:s");
	}
	
	// regra espeecificas do sistema********
	protected function BuscarIdDonoDoUsuario() {
		$sessao = $this->sessaoBuscar ();
		if ($sessao ['nivel'] == 1) {
			return null;
		} else if ($sessao ['nivel'] == 2) {
			return $this->DaUser->BuscarIdDonoDoUsuario ( $sessao ['id'], 'cliente' );
		} else if ($sessao ['nivel'] == 3) {
			return $this->DaUser->BuscarIdDonoDoUsuario ( $sessao ['id'], 'estabelecimento' );
		}
		return null;
	}
}
// END Controller class

/*
 * Tipo: Sucesso = 1, Aviso = 2, Advertencia =3, Erro = 4
*/

/* End of file Controller.php */
/* Location: ./system/core/Controller.php */