// Interfaz o Clase Abstracta base 
public abstract class Usuario
{
    public string Nombre { get; set; }
    public abstract string GetRol();
}

// Clases Concretas 
public class Voluntario : Usuario { public override string GetRol() => "Voluntario"; }
public class Coordinador : Usuario { public override string GetRol() => "Coordinador"; }

// La Fábrica 
public static class CreadorUsuario
{
    public static Usuario Crear(string tipo, string nombre)
    {
        Usuario nuevoUsuario = tipo switch
        {
            "Voluntario" => new Voluntario(),
            "Coordinador" => new Coordinador(),
            _ => throw new System.ArgumentException("Tipo no válido")
        };
        nuevoUsuario.Nombre = nombre;
        return nuevoUsuario;
    }
}