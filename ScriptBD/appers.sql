-- phpMyAdmin SQL Dump
-- version 4.8.0.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 14-07-2018 a las 21:10:50
-- Versión del servidor: 10.1.32-MariaDB
-- Versión de PHP: 7.2.5

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `appers`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `actor`
--

CREATE TABLE `actor` (
  `id_actor` int(11) NOT NULL,
  `nombre` varchar(32) DEFAULT NULL,
  `descripcion` text,
  `num_actual` int(11) DEFAULT NULL,
  `num_futuro` int(11) DEFAULT NULL,
  `num_de_contactables` int(11) DEFAULT NULL,
  `ref_proyecto` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `actor`
--

INSERT INTO `actor` (`id_actor`, `nombre`, `descripcion`, `num_actual`, `num_futuro`, `num_de_contactables`, `ref_proyecto`) VALUES
(423, 'manuel', 'udjfjjf', 366, 3666, 1, 2345),
(4223, 'kara', 'udjfjjf', 66, 666, 1, 2345),
(14223, 'macarena', 'udjfjjf', 266, 2666, 1, 2345),
(15426, 'marcelo', 'jkajkjkadjs', 12, 16, 2, 11),
(34223, 'perez', 'udjfjjf', 466, 4666, 1, 2345),
(40223, 'jose', 'udjfjjf', 166, 1666, 1, 5),
(42223, 'pez', 'udjfjjf', 566, 5666, 1, 5),
(42323, 'simon', 'udjfjjf', 666, 6666, 1, 11);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `asociacion`
--

CREATE TABLE `asociacion` (
  `req_usuario` int(11) NOT NULL,
  `req_software` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `asociacion`
--

INSERT INTO `asociacion` (`req_usuario`, `req_software`) VALUES
(9, 8),
(15, 12);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `caso_de_uso`
--

CREATE TABLE `caso_de_uso` (
  `id_caso_de_uso` int(11) NOT NULL,
  `ref_proyecto` int(11) NOT NULL,
  `ruta_imagen` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `caso_de_uso`
--

INSERT INTO `caso_de_uso` (`id_caso_de_uso`, `ref_proyecto`, `ruta_imagen`) VALUES
(1, 2345, 'C:UsersPublicDownloadsHolap.png'),
(10, 2345, 'C:UsersPublicDownloadsHolap.png'),
(12, 2345, 'C:UsersPublicDownloadsHolap.png'),
(13, 2345, 'C:UsersPublicDownloadsHolap.png'),
(100, 2345, 'C:UsersPublicDownloadsHolap.png'),
(122, 2345, 'C:UsersPublicDownloadsHolap.png');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `modificacion_ders`
--

CREATE TABLE `modificacion_ders` (
  `id_modificacion` int(11) NOT NULL,
  `version` float DEFAULT NULL,
  `ref_proyecto` int(11) DEFAULT NULL,
  `fecha` date DEFAULT NULL,
  `ref_autor_modificacion` varchar(12) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `proyecto`
--

CREATE TABLE `proyecto` (
  `id_proyecto` int(11) NOT NULL,
  `nombre` varchar(128) NOT NULL,
  `proposito` text,
  `alcance` text,
  `contexto` text,
  `definiciones` text,
  `acronimos` text,
  `abreviaturas` text,
  `referencias` text,
  `ambiente_operacional` text,
  `relacion_con_otros_proyectos` text
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `proyecto`
--

INSERT INTO `proyecto` (`id_proyecto`, `nombre`, `proposito`, `alcance`, `contexto`, `definiciones`, `acronimos`, `abreviaturas`, `referencias`, `ambiente_operacional`, `relacion_con_otros_proyectos`) VALUES
(5, 'las bolitas', 'diversión', 'todos', 'juegos', 'dff', 'juthh', 'efdfdf', 'bggg', 'iuytr', 'ninguno'),
(6, 'NASA', 'secreto', 'secreto', 'secreto', 'secreto', 'secreto', 'secreto', 'secreto', 'secreto', 'ninguno'),
(11, 'juego solo', 'cartas', 'todos', 'juegos de azar y mujerzuelas', 'jajaj', 'uduud', 'deee', 'wwww', 'wwwww', 'ninguna'),
(2345, 'appers', 'quien sabe', 'rusia', 'aplicacion de requisitos', 'hola', 'ho!', 'g', 'fg', 'f', 'fv'),
(345677777, 'fghgfhn', 'ghj', 'dcfvgbhn', 'ijbhuik', 'jhgyujkh', 'bhyujinhb', 'hujkbhujikm', 'hjmnbhjmnb', 'ghjnbvb', 'bhjkmnbhj');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `requisito`
--

CREATE TABLE `requisito` (
  `id_requisito` int(11) NOT NULL,
  `nombre` text NOT NULL,
  `descripcion` text,
  `prioridad` varchar(8) DEFAULT NULL,
  `categoria` varchar(12) DEFAULT NULL,
  `fuente` varchar(20) DEFAULT NULL,
  `estabilidad` varchar(11) DEFAULT NULL,
  `estado` varchar(13) DEFAULT NULL,
  `ref_proyecto` int(11) DEFAULT NULL,
  `tipo` varchar(8) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `requisito`
--

INSERT INTO `requisito` (`id_requisito`, `nombre`, `descripcion`, `prioridad`, `categoria`, `fuente`, `estabilidad`, `estado`, `ref_proyecto`, `tipo`) VALUES
(8, 'forma de bolitas', 'bolitas esfericas', 'CRITICA', 'FUNCIONAL', 'cliente', 'ESTABLE', 'CUMPLE', 5, 'SISTEMA'),
(9, 'elegir color de bolitas', 'todos los colores se eligen en un panel', 'DESEABLE', 'NO FUNCIONAL', 'cliente', 'NO ESTABLE', 'NO CUMPLE', 5, 'USUARIO'),
(10, 'las velas', 'nooooo', 'CRITICA', 'FUNCIONAL', 'profe', 'ESTABLE', 'NO CUMPLE', 2345, 'SISTEMA'),
(11, 'las manzanas', 'nooooo', 'CRITICA', 'FUNCIONAL', 'profe', 'ESTABLE', 'NO CUMPLE', 11, 'USUARIO'),
(12, 'les amigues', 'nooooo', 'CRITICA', 'FUNCIONAL', 'profe', 'ESTABLE', 'NO CUMPLE', 2345, 'SISTEMA'),
(15, 'azul', 'nooooo', 'CRITICA', 'FUNCIONAL', 'profe', 'ESTABLE', 'NO CUMPLE', 2345, 'SISTEMA'),
(16, 'laaaaa', 'nooooo', 'CRITICA', 'FUNCIONAL', 'profe', 'ESTABLE', 'NO CUMPLE', 11, 'SISTEMA'),
(23, 'comwer', 'nooooo', 'CRITICA', 'FUNCIONAL', 'profe', 'ESTABLE', 'NO CUMPLE', 2345, 'USUARIO');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `Id` varchar(128) NOT NULL,
  `Name` varchar(256) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `solicitud_vinculacion_proyecto`
--

CREATE TABLE `solicitud_vinculacion_proyecto` (
  `ref_proyecto` int(11) NOT NULL,
  `ref_solicitante` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `userclaims`
--

CREATE TABLE `userclaims` (
  `Id` int(11) NOT NULL,
  `UserId` varchar(128) NOT NULL,
  `ClaimType` longtext,
  `ClaimValue` longtext
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `userlogins`
--

CREATE TABLE `userlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `UserId` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `userroles`
--

CREATE TABLE `userroles` (
  `UserId` varchar(128) NOT NULL,
  `RoleId` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `Id` varchar(128) NOT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext,
  `SecurityStamp` longtext,
  `PhoneNumber` longtext,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEndDateUtc` datetime DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  `UserName` varchar(256) NOT NULL,
  `Rut` varchar(12) NOT NULL,
  `Tipo` varchar(20) NOT NULL,
  `Estado` bit(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `users`
--

INSERT INTO `users` (`Id`, `Email`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEndDateUtc`, `LockoutEnabled`, `AccessFailedCount`, `UserName`, `Rut`, `Tipo`, `Estado`) VALUES
('18171cef-3982-4b62-aaba-c630f5c650c1', 'fantasma@gmail.com', 0, 'AHmR4UGxaIoGi0EYseTFCjKyyIefsPoXg+z7+2CZFpjuBGMyLUASPEo2ZmBG6D17QQ==', '30cf8d93-549a-4999-bc52-c3fe011e5707', NULL, 0, 0, NULL, 1, 0, 'Fantasma', '12345678', 'SYSADMIN', b'1'),
('51d7b20a-55d8-4c9c-b0da-11e68d1e6b22', 'olfjh@gmail.com', 0, 'AAdpU+brwLSbEdxtxHVzC9U48jBllRMKa+C5kzh9NpjGgnWNaaBKG7WufOee1eRGcQ==', 'ce0b1a6a-0d01-40ae-9bce-b933ff3e4a7f', NULL, 0, 0, NULL, 1, 0, 'Carolina', '11783564', 'USUARIO', b'0'),
('5a1b4db9-85c6-48df-90c5-c1e33d7d3806', 'hishdjh@hotmail.com', 0, 'ACjk1x6wVIsg/s10Ato+HdkaCYmX2GxdSK+u5MDJ2wmcyxZT7MFRKbnPB4WqIFfUtQ==', 'd1c4e212-a062-4d61-9bf1-107adef0ea1d', NULL, 0, 0, NULL, 1, 0, 'Manuel', '18892403', 'SYSADMIN', b'1'),
('8ffff674-2318-4ac2-b292-c308957d2d12', 'jahsh@hotmail.com', 0, 'ANSCxa4ZWAUvbgnOa6LMVeRGLwXhSdnEzlz5wjHaHX8ybFsZlFV8tn6rKsmdRRpvuw==', 'a266859d-1660-4c88-b19e-85213c8bf997', NULL, 0, 0, NULL, 1, 0, 'Matias', '19016777', 'USUARIO', b'1'),
('a695ea78-8fe4-4e69-bf7c-3e16284ddb54', 'ishufhijjf@hotmail.es', 0, 'AP9YYUSqbeoS0OOkHNlmqhkKFAcNcR06lFSRoFZwGlQINu2B41pJ1rSq1LLpVgMjIQ==', 'd6fbd7d1-9f6a-4d96-b784-05ad320c89c8', NULL, 0, 0, NULL, 1, 0, 'Elisa', '82675364', 'SYSADMIN', b'1'),
('a77358c3-1eb2-4845-b97c-37cefa873cd5', 'ihdfh@gmail.com', 0, 'AB8gkn+ycaDu4Z5dXnVxav9GYzpUpOB1qBA7ShArUcePi2mnMEkSMBD+uOWQqzwLrw==', '0bdcfcc8-a4a2-4ab4-98da-2936127a5f91', NULL, 0, 0, NULL, 1, 0, 'Christian', '18568263', 'SYSADMIN', b'1'),
('bf040feb-5f81-4d7c-8394-7e7f1faddf52', 'omfgh@gmail.com', 0, 'AGK7DQ3XzF/hLV81AxpLXpaw7VZheZvImekVXbRGvabOMcf6aGu4ICyRPcC5l7aNVQ==', 'dbff2531-28c3-4b68-b8b3-ae2fbfd281b9', NULL, 0, 0, NULL, 1, 0, 'Patricio', '78635487', 'SYSADMIN', b'0'),
('fa1143a1-4ee7-4cb0-b0fc-3e0c906da60a', 'tjeodh@gmail.cl', 0, 'AJgr5ZCV8/n/i6aUlr6Cv6jtJxiddtwp2TGYVks4LOxn/+YT9Ib650m75Vvg6U276A==', '8fd67706-4b48-4863-b898-2356f65afa5c', NULL, 0, 0, NULL, 1, 0, 'Gerardo', '18625374', 'SYSADMIN', b'1');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vinculo_actor_requisito`
--

CREATE TABLE `vinculo_actor_requisito` (
  `ref_actor` int(11) NOT NULL,
  `ref_req` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `vinculo_usuario_proyecto`
--

CREATE TABLE `vinculo_usuario_proyecto` (
  `ref_usuario` varchar(128) NOT NULL,
  `ref_proyecto` int(11) NOT NULL,
  `rol` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Volcado de datos para la tabla `vinculo_usuario_proyecto`
--

INSERT INTO `vinculo_usuario_proyecto` (`ref_usuario`, `ref_proyecto`, `rol`) VALUES
('5a1b4db9-85c6-48df-90c5-c1e33d7d3806', 2345, 'USUARIO'),
('8ffff674-2318-4ac2-b292-c308957d2d12', 2345, 'JEFEPROYECTO');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `actor`
--
ALTER TABLE `actor`
  ADD PRIMARY KEY (`id_actor`),
  ADD KEY `ref_proyecto5` (`ref_proyecto`);

--
-- Indices de la tabla `asociacion`
--
ALTER TABLE `asociacion`
  ADD KEY `asociacion_1` (`req_usuario`),
  ADD KEY `asociacion_2` (`req_software`);

--
-- Indices de la tabla `caso_de_uso`
--
ALTER TABLE `caso_de_uso`
  ADD PRIMARY KEY (`id_caso_de_uso`),
  ADD KEY `ref_proyecto4` (`ref_proyecto`);

--
-- Indices de la tabla `modificacion_ders`
--
ALTER TABLE `modificacion_ders`
  ADD PRIMARY KEY (`id_modificacion`),
  ADD KEY `ref_proyecto6` (`ref_proyecto`),
  ADD KEY `referencia_autor` (`ref_autor_modificacion`);

--
-- Indices de la tabla `proyecto`
--
ALTER TABLE `proyecto`
  ADD PRIMARY KEY (`id_proyecto`);

--
-- Indices de la tabla `requisito`
--
ALTER TABLE `requisito`
  ADD PRIMARY KEY (`id_requisito`),
  ADD KEY `ref_proyecto3` (`ref_proyecto`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `solicitud_vinculacion_proyecto`
--
ALTER TABLE `solicitud_vinculacion_proyecto`
  ADD KEY `referencia_solicitante` (`ref_solicitante`),
  ADD KEY `ref_proyecto2` (`ref_proyecto`);

--
-- Indices de la tabla `userclaims`
--
ALTER TABLE `userclaims`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Id` (`Id`),
  ADD KEY `UserId` (`UserId`);

--
-- Indices de la tabla `userlogins`
--
ALTER TABLE `userlogins`
  ADD PRIMARY KEY (`LoginProvider`,`ProviderKey`,`UserId`),
  ADD KEY `ApplicationUser_Logins` (`UserId`);

--
-- Indices de la tabla `userroles`
--
ALTER TABLE `userroles`
  ADD PRIMARY KEY (`UserId`,`RoleId`),
  ADD KEY `IdentityRole_Users` (`RoleId`);

--
-- Indices de la tabla `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `vinculo_actor_requisito`
--
ALTER TABLE `vinculo_actor_requisito`
  ADD KEY `referencia_req_usuario_idx` (`ref_req`),
  ADD KEY `referencia_actor` (`ref_actor`);

--
-- Indices de la tabla `vinculo_usuario_proyecto`
--
ALTER TABLE `vinculo_usuario_proyecto`
  ADD KEY `referencia_usuario` (`ref_usuario`),
  ADD KEY `ref_proyecto1` (`ref_proyecto`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `userclaims`
--
ALTER TABLE `userclaims`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `actor`
--
ALTER TABLE `actor`
  ADD CONSTRAINT `ref_proyecto5` FOREIGN KEY (`ref_proyecto`) REFERENCES `proyecto` (`id_proyecto`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `asociacion`
--
ALTER TABLE `asociacion`
  ADD CONSTRAINT `asociacion_1` FOREIGN KEY (`req_usuario`) REFERENCES `requisito` (`id_requisito`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `asociacion_2` FOREIGN KEY (`req_software`) REFERENCES `requisito` (`id_requisito`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `caso_de_uso`
--
ALTER TABLE `caso_de_uso`
  ADD CONSTRAINT `ref_proyecto4` FOREIGN KEY (`ref_proyecto`) REFERENCES `proyecto` (`id_proyecto`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `modificacion_ders`
--
ALTER TABLE `modificacion_ders`
  ADD CONSTRAINT `ref_proyecto6` FOREIGN KEY (`ref_proyecto`) REFERENCES `proyecto` (`id_proyecto`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `referencia_autor` FOREIGN KEY (`ref_autor_modificacion`) REFERENCES `users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `requisito`
--
ALTER TABLE `requisito`
  ADD CONSTRAINT `ref_proyecto3` FOREIGN KEY (`ref_proyecto`) REFERENCES `proyecto` (`id_proyecto`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `solicitud_vinculacion_proyecto`
--
ALTER TABLE `solicitud_vinculacion_proyecto`
  ADD CONSTRAINT `ref_proyecto2` FOREIGN KEY (`ref_proyecto`) REFERENCES `proyecto` (`id_proyecto`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `referencia_solicitante` FOREIGN KEY (`ref_solicitante`) REFERENCES `users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `userclaims`
--
ALTER TABLE `userclaims`
  ADD CONSTRAINT `ApplicationUser_Claims` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Filtros para la tabla `userlogins`
--
ALTER TABLE `userlogins`
  ADD CONSTRAINT `ApplicationUser_Logins` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Filtros para la tabla `userroles`
--
ALTER TABLE `userroles`
  ADD CONSTRAINT `ApplicationUser_Roles` FOREIGN KEY (`UserId`) REFERENCES `users` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `IdentityRole_Users` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE ON UPDATE NO ACTION;

--
-- Filtros para la tabla `vinculo_actor_requisito`
--
ALTER TABLE `vinculo_actor_requisito`
  ADD CONSTRAINT `referencia_actor` FOREIGN KEY (`ref_actor`) REFERENCES `actor` (`id_actor`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `referencia_req` FOREIGN KEY (`ref_req`) REFERENCES `requisito` (`id_requisito`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `vinculo_usuario_proyecto`
--
ALTER TABLE `vinculo_usuario_proyecto`
  ADD CONSTRAINT `ref_proyecto1` FOREIGN KEY (`ref_proyecto`) REFERENCES `proyecto` (`id_proyecto`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `referencia_usuario` FOREIGN KEY (`ref_usuario`) REFERENCES `users` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
