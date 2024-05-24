<?php
include "config.php";
include "utils.php";

$dbConn =  connect($db);

if ($_SERVER['REQUEST_METHOD'] == 'GET')
{
    if (isset($_GET['IdCancha']))
    {
      //Mostrar un post
      $sql = $dbConn->prepare("SELECT * FROM rc_cancha where IdCancha=:IdCancha");
      $sql->bindValue(':IdCancha', $_GET['IdCancha']);
      $sql->execute();
      header("HTTP/1.1 200 OK");
      echo json_encode(  $sql->fetch(PDO::FETCH_ASSOC)  );
      exit();
	  }
else {
      //Mostrar lista de post
      $sql = $dbConn->prepare("SELECT * FROM rc_cancha");
      $sql->execute();
      $sql->setFetchMode(PDO::FETCH_ASSOC);
      header("HTTP/1.1 200 OK");
      echo json_encode( $sql->fetchAll()  );
      exit();
	}
}
