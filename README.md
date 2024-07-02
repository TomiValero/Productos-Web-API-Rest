
</head>
<body>
    <h1>Web API Project</h1>
    
  <h2>Descripción</h2>
  <p>
      Esta es una Web API desarrollada utilizando .NET Core que implementa una arquitectura en capas para manejar artículos, marcas y categorías, proporcionando todas las operaciones CRUD (Crear, Leer, Actualizar, Eliminar). La aplicación utiliza Entity Framework Data First para la conexión a la base de datos y hace uso de la inyección de dependencias para la gestión de las dependencias entre las capas.
  </p>
 
  <h2>Estructura del Proyecto</h2>
  <ul>
      <li><strong>Controllers:</strong> Contiene los controladores de la API que manejan las solicitudes HTTP y devuelven las respuestas.</li>
      <li><strong>DTOs:</strong> Data Transfer Objects utilizados para transferir datos entre las capas.</li>
      <li><strong>Models:</strong> Contiene las clases de modelo generadas por Entity Framework a partir de la base de datos.</li>
      <li><strong>Properties:</strong> Contiene los archivos de configuración del proyecto.</li>
      <li><strong>Repository:</strong> Implementa la lógica de acceso a datos y la conexión con la base de datos.</li>
      <li><strong>Services:</strong> Contiene la lógica de negocio de la aplicación.</li>
      <li><strong>Validators:</strong> Contiene la lógica de validación de datos.</li>
  </ul>

  <h2>Tecnologías Utilizadas</h2>
  <ul>
      <li><strong>.NET Core:</strong> Framework para el desarrollo de la API.</li>
      <li><strong>Entity Framework:</strong> ORM utilizado para la conexión y manipulación de la base de datos.</li>
      <li><strong>Inyección de Dependencias:</strong> Utilizado para gestionar las dependencias entre las capas.</li>
  </ul>
</body>
</html>


