<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['IdReserva']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * FROM rc_reservas where IdReserva=:IdReserva");
      $sql->bindValue(':IdReserva', $_GET['IdReserva']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
	  }
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM rc_reservas");
      $sql->execute();
      $sql->setFetchMode(PDO::FETCH_ASSOC);
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
	}
}

if ($_SERVER['REQUEST_METHOD'] == 'POST')
{
    $input = $_POST;
    $sql = "INSERT INTO rc_reservas
          (IdUsuario, IdCancha, FechaReserva, HoraEntrada, HoraSalida, Estado)
          VALUES
          ( :IdUsuario, :IdCancha, :FechaReserva, :HoraEntrada, :HoraSalida, :Estado)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if($postCodigo)
    {
      $input['IdReserva'] = $postCodigo;
      header("HTTP/1.1 200 OK");
      echo json_encode($input);
      exit();
  }
}

if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$codigo = $_GET['IdReserva'];
  $statement = $dbConn->prepare("DELETE FROM  rc_reservas where IdReserva=:IdReserva");
  $statement->bindValue(':IdReserva', $codigo);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}


if ($_SERVER['REQUEST_METHOD'] == 'PUT')
{
    $input = $_GET;
    $postCodigo = $input['IdReserva'];
    $fields = getParams($input);

    $sql = "
          UPDATE rc_reservas
          SET $fields
          WHERE IdReserva='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

?>