<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    if (isset($_GET['IdCalificacion'])) {
        //Mostrar un post
        $sql = $dbConn->prepare("SELECT * FROM rc_calificacion where IdCalificacion=:codigo");
        $sql->bindValue(':codigo', $_GET['IdCalificacion']);
        $sql->execute();
        header("HTTP/1.1 200 OK");
        echo json_encode($sql->fetch(PDO::FETCH_ASSOC));
        exit();
    } else {
        //Mostrar lista de post
        $sql = $dbConn->prepare("SELECT * FROM rc_calificacion");
        $sql->execute();
        $sql->setFetchMode(PDO::FETCH_ASSOC);
        header("HTTP/1.1 200 OK");
        echo json_encode($sql->fetchAll());
        exit();
    }
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $input = $_POST;
    $sql = "INSERT INTO rc_calificacion
          (IdReserva, Puntaje, Comentario, Fecha)
          VALUES
          (:idreserva, :puntaje, :comentario, :fecha)";
    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);
    $statement->execute();

    $postCodigo = $dbConn->lastInsertId();
    if ($postCodigo) {
        $input['codigo'] = $postCodigo;
        header("HTTP/1.1 200 OK");
        echo json_encode($input);
        exit();
    }
}
if ($_SERVER['REQUEST_METHOD'] == 'DELETE')
{
	$codigo = $_GET['IdCalificacion'];
  $statement = $dbConn->prepare("DELETE FROM  rc_calificacion where IdCalificacion=:IdCalificacion");
  $statement->bindValue(':IdCalificacion', $codigo);
  $statement->execute();
	header("HTTP/1.1 200 OK");
	exit();
}
