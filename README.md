# PruebaTecnicaConexusIT
Prueba tecnica ConexusIT


## Diseño de la Api
Se creó la api teniendo en cuenta la siguiente estructura:
- Manejo de 4 capas(Api-Appliation-Domain-Persistencia)
- Manejo de la UnitOfWork, para más seguridad y evitar que se hagan instancias inecesarias o repetidas de una clase.
- Repositorios Genericos e Interfaces, para manejar herencia de funciones que se utulizaran en todas los modelos.
- DTOS, para personalizar que y como se le va a mostrar al usuario la información que el vaya a solicitar de los endpoints.
- Se Configuró el rateLimiting, con el fin de crear un lapso de tiempo en el que el usaurio podra realziar peticiones a los endpoints.