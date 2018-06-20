--Deshabilitar una cuenta de usuario.
--Es necesaria una query anterior que capture el rut del usuario (un Select).
UPDATE Usuario SET 
Usuario.estado = 'deshabilitado'
WHERE Usuario.rut = 'rut';
