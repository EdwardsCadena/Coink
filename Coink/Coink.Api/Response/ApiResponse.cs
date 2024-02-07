// La clase ApiResponse es una clase genérica. Esto significa que puedo 
// usarla con cualquier tipo de datos. T es un marcador de posición para 
// el tipo de datos que quiero devolver en la respuesta de la API.
public class ApiResponse<T>
{
    // Este es el constructor de la clase. Se llama cuando creas una nueva 
    // instancia de ApiResponse. Acepta un argumento, que es el dato que 
    // quieres incluir en la respuesta de la API.
    public ApiResponse(T data)
    {
        // Aquí se asigna el dato proporcionado a la propiedad Data de la clase.
        Data = data;
    }

    // Esta es la propiedad Data de la clase. Almacena los datos que quieres 
    // devolver en la respuesta de la API. Su tipo es T, por lo que puede ser 
    // cualquier tipo de datos.
    public T Data { get; set; }
}