CREATE DATABASE `bitacora` /*!40100 DEFAULT CHARACTER SET latin1 */;

DROP TABLE IF EXISTS `bitacora`.`adjuntos`;
CREATE TABLE  `bitacora`.`adjuntos` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `identrada` int(10) unsigned NOT NULL,
  `nombre` char(255) NOT NULL,
  `datos` longblob NOT NULL,
  `tamanio` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`entradas`;
CREATE TABLE  `bitacora`.`entradas` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `fecha` datetime NOT NULL,
  `maquina` int(10) unsigned NOT NULL,
  `descripcion` char(255) NOT NULL,
  `contenido` blob NOT NULL,
  `tamanio` int(10) unsigned NOT NULL,
  `jefearea` int(10) unsigned NOT NULL,
  `fecha_inicio` datetime NOT NULL,
  `fecha_fin` datetime NOT NULL,
  `usuario` int(10) unsigned NOT NULL,
  `version` char(19) NOT NULL,
  `bloqueada` tinyint(1) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`leidos`;
CREATE TABLE  `bitacora`.`leidos` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `identrada` int(10) unsigned NOT NULL,
  `idusuario` int(10) unsigned NOT NULL,
  `fecha_leido` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`maquinas`;
CREATE TABLE  `bitacora`.`maquinas` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `descripcion` char(30) NOT NULL,
  `marca` char(20) NOT NULL,
  `modelo` char(20) NOT NULL,
  `no_serie` char(20) NOT NULL,
  `fecha_fabricacion` datetime NOT NULL,
  `fecha_registro` datetime NOT NULL,
  `ubicacion` char(30) NOT NULL,
  `comentarios` char(40) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`preferencias`;
CREATE TABLE  `bitacora`.`preferencias` (
  `Id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Usuario` int(10) unsigned NOT NULL,
  `Seccion` varchar(45) NOT NULL,
  `Clave` varchar(45) NOT NULL,
  `Valor` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`referenciaentradas`;
CREATE TABLE  `bitacora`.`referenciaentradas` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `identrada` int(10) unsigned NOT NULL,
  `idreferencia` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`referenciamaquinas`;
CREATE TABLE  `bitacora`.`referenciamaquinas` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `identrada` int(10) unsigned NOT NULL,
  `idmaquina` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`responsables`;
CREATE TABLE  `bitacora`.`responsables` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `identrada` int(10) unsigned NOT NULL,
  `idresponsable` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

DROP TABLE IF EXISTS `bitacora`.`usuarios`;
CREATE TABLE  `bitacora`.`usuarios` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `idusuarioalta` int(10) unsigned NOT NULL,
  `nombre` char(40) NOT NULL,
  `no_empleado` int(10) unsigned NOT NULL,
  `palabra_ingreso` char(41) NOT NULL,
  `fecha_registro` datetime NOT NULL,
  `fecha_login` datetime NOT NULL,
  `version` char(19) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;