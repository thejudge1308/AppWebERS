--Actualizar Cuenta de usuario.
--Es necesaria una query anterior que capture el rut del usuario (un Select).
UPDATE Usuario SET 

Usuario.nombre = 'nombre',
Usuario.correo_electronico = 'correo_electronico',
Usuario.contrasenia = 'contrasenia',
Usuario.tipo = 'tipo'

WHERE Usuario.rut = 'rut';
