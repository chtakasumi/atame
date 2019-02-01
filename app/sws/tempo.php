<!doctype html>
<html>
<head>
<meta charset="utf-8">
<title>Documento sem título</title>
</head>

<body>
<p>
  <?php 
//teste
$for = '1.666666666666667';
$tempo = $_POST['m'];

$res = $for*$tempo;

 
?>
  
  
</p>
<form id="form1" name="form1" method="post">
  <table width="100%" border="0" cellspacing="2" cellpadding="2">
    <tr>
      <th width="25%" style="text-align: left" scope="row"><strong>Digite a hora em minutos </strong></th>
      <td width="9%">&nbsp;</td>
      <td width="66%" rowspan="2"><input name="m" type="text" id="m" placeholder="Digite somente numeros em minutos " size="40"></td>
    </tr>
    <tr>
      <th style="text-align: left" scope="row">15 minutos =&gt; 1/4 de hora</th>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <th style="text-align: left" scope="row"><strong>30 minutos =&gt; Meia hora</strong></th>
      <td>&nbsp;</td>
      <td rowspan="2"><input type="submit" name="submit" id="submit" value="Calcular"></td>
    </tr>
    <tr>
      <th style="text-align: left" scope="row"><strong>60 minutos =&gt; 1 hora</strong></th>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <th style="text-align: left" scope="row"><strong>90 minutos =&gt; 1 hora e meia</strong></th>
      <td>&nbsp;</td>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <th colspan="2" style="text-align: left" scope="row">120 minutos =&gt; 2 horas</th>
      <td><h4><span style="text-align: left">Resultado para colocar no campo hora do SISCO</span> <span style="color: rgba(255,0,0,1)"><strong style="color: rgba(255,0,0,1)"><h1><?php echo $res; ?></h1>&nbsp;</strong></span></h4></td>
    </tr>
    <tr>
      <th colspan="2" style="text-align: left" scope="row">150 minutos =&gt; 2 horas e meia </th>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <th colspan="2" style="text-align: left" scope="row">&nbsp;</th>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <th colspan="3" style="text-align: left" scope="row"><p>&nbsp;</p>
        <p>Obs. Digite apenas minutos no campo para calcular. </p>
      <p>Usar somente ate as quatro primeiras casas da resposta para colocar no sistema SISCO</p></th>
    </tr>
  </table>
</form>
<p>&nbsp;</p>
</body>
</html>