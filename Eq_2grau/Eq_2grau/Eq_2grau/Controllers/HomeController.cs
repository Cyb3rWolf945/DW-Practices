using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Eq_2grau.Models;

namespace Eq_2grau.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Função de calculo de raizes de uma eq. 2º grau.
    ///
    /// </summary>
    /// <param name="a">Coeficiente do parametro X²</param>
    /// <param name="b">Coeficiente do parametro X</param>
    /// <param name="c">Coeficiente do parametro independente</param>
    /// <returns></returns>
    public IActionResult Index(string a, string b, string c)
    {
        /* ALGORITMO
         * 1. Determinar se os parametros fornecidos são números.
         *      se sim,
         *          2. Determinar se (A <> 0) (A != 0)
         *              se nao:
         *                  3. Enviar mensagem de erro para o utilizador.
         * 
         *              se sim: 
         *                  3. Calcular as Raizes
         *                          X1, X2 = ((-b +- sqrt(b^2-4ac))/ (2 * a)
         *
         *      se nao,
         *          enviar mensagem de erro para o utilizador.
         * 
         * 
         */
        
        if (String.IsNullOrEmpty(a) || String.IsNullOrEmpty(b) || String.IsNullOrEmpty(c))
        {
            ViewBag.Mensagem = "Os elementos A, B e C têm de ser preenchidos";
            return View();
        }

        if (!Double.TryParse(a, out double newA) || !Double.TryParse(b, out double newB) || !Double.TryParse(c, out double newC))
        {
            ViewBag.Mensagem = "Os coeficientes fornecidos não são números válidos";
            return View();
        }

        if (newA == 0)
        {
            ViewBag.Mensagem = "O parametro A não pode ser zero!";
            return View();
        }

        double delta = Math.Pow(newB, 2) - 4 * newA * newC;

        if (delta > 0)
        {
            double x1 = (-newB + Math.Sqrt(delta)) / (2 * newA);
            double x2 = (-newB - Math.Sqrt(delta)) / (2 * newA);
            ViewBag.Mensagem = "Existem 2 raizes reais distintas.";
            ViewBag.x1 = x1;
            ViewBag.x2 = x2;
        }
        else if (delta == 0)
        {
            double x1 = -newB / (2 * newA);
            ViewBag.Mensagem = "Existem 2 raizes reais iguais.";
            ViewBag.x1 = x1;
            ViewBag.x2 = x1;
        }
        else
        {
            // Raizes complexas conjugadas
            double realPart = -newB / (2 * newA);
            double imaginaryPart = Math.Sqrt(-delta) / (2 * newA);
            ViewBag.Mensagem = "Existem 2 raizes complexas conjugadas.";
            ViewBag.x1 = $"{realPart} + {imaginaryPart}i";
            ViewBag.x2 = $"{realPart} - {imaginaryPart}i";
        }

        return View();
    
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}