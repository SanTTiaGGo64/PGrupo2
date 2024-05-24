<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET') {
    if (isset($_GET['IdUsuario'])) {
        //Mostrar un post
        $sql = $dbConn->prepare("SELECT * FROM rc_usuarios where IdUsuario=:codigo");
        $sql->bindValue(':codigo', $_GET['IdUsuario']);
        $sql->execute();
        header("HTTP/1.1 200 OK");
        echo json_encode($sql->fetch(PDO::FETCH_ASSOC));
        exit();
    } else {
        //Mostrar lista de post
        $sql = $dbConn->prepare("SELECT * FROM rc_usuarios");
        $sql->execute();
        $sql->setFetchMode(PDO::FETCH_ASSOC);
        header("HTTP/1.1 200 OK");
        echo json_encode($sql->fetchAll());
        exit();
    }
}

if ($_SERVER['REQUEST_METHOD'] == 'POST') {
    $input = $_POST;
    $sql = "INSERT INTO rc_usuarios
          (Nombre, Apellido, Correo, Contraseña, Telefono)
          VALUES
          (:nombre, :apellido, :correo, :contrasena, :telefono)";
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

if ($_SERVER['REQUEST_METHOD'] == 'DELETE') {
    $codigo = $_GET['IdUsuario'];
    $statement = $dbConn->prepare("DELETE FROM  rc_usuarios where IdUsuario=:codigo");
    $statement->bindValue(':codigo', $codigo);
    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}

if ($_SERVER['REQUEST_METHOD'] == 'PUT') {
    $input = $_GET;
    $postCodigo = $input['IdUsuario'];
    $fields = getParams($input);

    $sql = "
          UPDATE rc_usuarios
          SET $fields
          WHERE IdUsuario='$postCodigo'
           ";

    $statement = $dbConn->prepare($sql);
    bindAllValues($statement, $input);

    $statement->execute();
    header("HTTP/1.1 200 OK");
    exit();
}
