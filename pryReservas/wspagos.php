<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    if (isset($_GET['IdPago'])) {
        //Mostrar un post
        $sql = $dbConn->prepare("SELECT * FROM rc_pagos where IdPago=:codigo");
        $sql->bindValue(':codigo', $_GET['IdPago']);
        $sql->execute();
        header("HTTP/1.1 200 OK");
        echo json_encode($sql->fetch(PDO::FETCH_ASSOC));
        exit();
    } else {
        //Mostrar lista de post
        $sql = $dbConn->prepare("SELECT * FROM rc_pagos");
        $sql->execute();
        $sql->setFetchMode(PDO::FETCH_ASSOC);
        header("HTTP/1.1 200 OK");
        echo json_encode($sql->fetchAll());
        exit();
    }
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $input = $_POST;
    $sql = "INSERT INTO rc_pagos
          (IdReserva, MontoPagado, MontoRestante, FechaPago, Metodo)
          VALUES
          (:idreserva, :montopagado, :montorestante, :fechapago, :metodo)";
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
    $codigo = $_GET['IdPago'];
  $statement = $dbConn->prepare("DELETE FROM  rc_pagos where IdPago=:IdPago");
  $statement->bindValue(':IdPago', $codigo);
  $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
