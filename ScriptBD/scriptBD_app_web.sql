-- MySQL Script generated by MySQL Workbench
-- Tue Jun 19 00:45:49 2018
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering



-- -----------------------------------------------------
-- Schema appers
-- -----------------------------------------------------
CREATE DATABASE IF NOT EXISTS `appers`;
USE `appers`;



-- -----------------------------------------------------
-- Table `appers`.`Usuario`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Usuario` (
  `rut` VARCHAR(12) NOT NULL,
  `nombre` VARCHAR(50) NOT NULL,
  `correo_electronico` VARCHAR(35) NOT NULL,
  `contrasenia` VARCHAR(50) NOT NULL,
  `tipo` VARCHAR(20) NOT NULL,
  `estado` BIT NOT NULL,
  PRIMARY KEY (`rut`)
  );


-- -----------------------------------------------------
-- Table `appers`.`Proyecto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Proyecto` (
  `id_proyecto` INT NOT NULL AUTO_INCREMENT,
  `nombre` VARCHAR(128) NULL,
  `proposito` TEXT NULL,
  `alcance` TEXT NULL,
  `contexto` TEXT NULL,
  `definiciones` TEXT NULL,
  `acronimos` TEXT NULL,
  `abreviaturas` TEXT NULL,
  `referencias` TEXT NULL,
  `ambiente_operacional` TEXT NULL,
  `relacion_con_otros_proyectos` TEXT NULL,
  PRIMARY KEY (`id_proyecto`)
  );


-- -----------------------------------------------------
-- Table `appers`.`Modificacion_DERS`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Modificacion_DERS` (
  `id_modificacion` INT NOT NULL,
  `version` FLOAT NOT NULL,
  `ref_proyecto` INT NOT NULL,
  `fecha` DATE NULL,
  `ref_autor_modificacion` VARCHAR(12) NOT NULL,
  PRIMARY KEY (`id_modificacion`),
  FOREIGN KEY (`ref_proyecto`)
  REFERENCES `appers`.`Proyecto` (`id_proyecto`),
  FOREIGN KEY (`ref_autor_modificacion`)
  REFERENCES `appers`.`Usuario` (`rut`)
  );


-- -----------------------------------------------------
-- Table `appers`.`Vinculo_usuario_proyecto`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Vinculo_usuario_proyecto` (
  `ref_usuario` VARCHAR(12) NOT NULL,
  `ref_proyecto` INT NOT NULL,
  `rol` VARCHAR(30) NOT NULL,
    FOREIGN KEY (`ref_usuario`)
    REFERENCES `appers`.`Usuario` (`rut`),
    FOREIGN KEY (`ref_proyecto`)
    REFERENCES `appers`.`Proyecto` (`id_proyecto`)

  );


-- -----------------------------------------------------
-- Table `appers`.`Caso_de_uso`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Caso_de_uso` (
  `id_caso_de_uso` INT NOT NULL,
  `ref_proyecto` INT NOT NULL,
  `ruta_imagen` TEXT NULL,
  PRIMARY KEY (`id_caso_de_uso`),
    FOREIGN KEY (`ref_proyecto`)
    REFERENCES `appers`.`Proyecto` (`id_proyecto`)
    );


-- -----------------------------------------------------
-- Table `appers`.`Actor`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Actor` (
  `id_actor` INT NOT NULL,
  `nombre` VARCHAR(32) NOT NULL,
  `descripcion` TEXT NULL,
  `num_actual` INT NULL,
  `num_futuro` INT NULL,
  `num_de_contactables` INT NULL,
  `ref_proyecto` INT NOT NULL,
  PRIMARY KEY (`id_actor`),
    FOREIGN KEY (`ref_proyecto`)
    REFERENCES `appers`.`Proyecto` (`id_proyecto`)
    );


-- -----------------------------------------------------
-- Table `appers`.`Requisito`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Requisito` (
  `id_requisito` INT NOT NULL,
  `nombre` TEXT NULL,
  `descripcion` TEXT NULL,
  `prioridad` VARCHAR(8) NULL,
  `categoria` VARCHAR(12) NULL,
  `fuente` VARCHAR(20) NULL,
  `estabilidad` VARCHAR(11) NULL,
  `estado` VARCHAR(13) NULL,
  `ref_proyecto` INT NULL,
  `tipo` VARCHAR(8) NULL,
  PRIMARY KEY (`id_requisito`),
    FOREIGN KEY (`ref_proyecto`)
    REFERENCES `appers`.`Proyecto` (`id_proyecto`)
    );

-- -----------------------------------------------------
-- Table `appers`.`Asociacion`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Asociacion` (
  `req_usuario` INT NOT NULL,
  `req_software` INT NOT NULL,
    FOREIGN KEY (`req_usuario`)
    REFERENCES `appers`.`Requisito` (`id_requisito`),
    FOREIGN KEY (`req_software`)
    REFERENCES `appers`.`Requisito` (`id_requisito`)
    );


-- -----------------------------------------------------
-- Table `appers`.`Vinculo_actor_requisito`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `appers`.`Vinculo_actor_requisito` (
  `ref_actor` INT NOT NULL,
  `ref_req` INT NOT NULL,
    FOREIGN KEY (`ref_actor`)
    REFERENCES `appers`.`Actor` (`id_actor`),
    FOREIGN KEY (`ref_req`)
    REFERENCES `appers`.`Requisito` (`id_requisito`)
    );
