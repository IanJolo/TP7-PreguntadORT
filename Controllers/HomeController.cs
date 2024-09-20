using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TP7_PreguntadORT.Models;

namespace TP7_PreguntadORT.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View("Index");
    }

    public IActionResult ConfigurarJuego(){
        Juegos.InicializarJuego();
        ViewBag.Categorias=BD.ObtenerCategorias();
        ViewBag.Dificultades=BD.ObtenerDificultades();
        return View("ConfigurarJuego");
    }

    public IActionResult Comenzar( int dificultad, int categoria){
        Dictionary<int, List<Preguntas>> preguntas=Juegos.CargarPartida(dificultad,categoria);
        if(preguntas==null){
        return Redirect("ConfigurarJuego");
        }else {
            return Redirect(Url.Action("Jugar", "Home", new {categoria=categoria}));
        }
    }
    public IActionResult Creditos(){
        return View("Creditos");
    }
    public IActionResult RankingGlobal(){
        return View("RankingGlobal");
    }
    public IActionResult InicioSesion(){
        return View("Ingreso");
    }
    public IActionResult ValidarInicio(string nombreUsuario, string foto){
        BD.InicioSesion(nombreUsuario, foto);
        return Redirect(Url.Action("LandingPage", "Home", new{nombreUsuario}));
    }
    public IActionResult LandingPage(string nombreUsuario){
        ViewBag.DatosUsuario=BD.ObtenerInfoUsuario(nombreUsuario);
        if(ViewBag.DatosUsuario.puntaje==null){
            ViewBag.DatosUsuario.puntaje=0;
        }
        if(ViewBag.DatosUsuario.racha==null){
            ViewBag.DatosUsuario.racha=0;
        }
        return View("LandingPage");
    }
     public IActionResult Jugar(int categoria){
        Console.WriteLine(categoria);
        if(categoria==-1){
            return View("Juego");
        }else{
            return Redirect(Url.Action("MomentoPreg", "Home"));
            
        }
        
        
       
    }

    public IActionResult MomentoPreg(string nombrecategoria){
        int idCategoRuleta=1;
        if(nombrecategoria=="HISTORIA"){
            idCategoRuleta=2;
        }else if(nombrecategoria=="CIENCIA"){
            idCategoRuleta=3;
        }else if(nombrecategoria=="DEPORTE"){
            idCategoRuleta=4;
        }else if(nombrecategoria=="GEOGRAFIA"){
            idCategoRuleta=1;
        }else {
            Console.WriteLine("ERROR");
        }
        ViewBag.Pregunta=Juegos.ObtenerProximaPregunta(idCategoRuleta);
         if(ViewBag.Pregunta==null){
            return View("Fin"); 
         }else {
            ViewBag.Respuestas=Juegos.ObtenerProximasRespuestas(ViewBag.Pregunta.IdPregunta);      
        return View("MomentoPreg");
        }
    }
[HttpPost]
public IActionResult VerificarRespuesta(int idPregunta, int idRespuesta){
    List<Respuestas> listaRespuestas=Juegos.ObtenerRespuestas();
    foreach(Respuestas respuesta in ViewBag.Respuestas){
        if(respuesta.Correcta==true){
            ViewBag.RespuestaCorrecta=respuesta;
        }else {
            return View("Fin");
        }
    }
    ViewBag.FueCorrecta=Juegos.VerificarRespuesta(idPregunta,idRespuesta);
    return View("Respuesta");
}

public IActionResult Juego(){
    return View("Juego");
}











    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
