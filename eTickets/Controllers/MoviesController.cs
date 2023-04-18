using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allmovies = await _service.GetAllAsync(o => o.Cinema);
            return View(allmovies);
        }

        //GET: Movie/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetauils = await _service.GetMovieByIdAsync(id);
            return View(movieDetauils);
        }

        //GET: Movies/Creat
        public async Task<IActionResult> Create()
        {
            var moievDropdownsData = await _service.GetMovieDropdownsValues();
            ViewBag.Actors = new SelectList(moievDropdownsData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(moievDropdownsData.Producers, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(moievDropdownsData.Cinemas, "Id", "Name");

            return View();
        }

        //POST : Movie/Create
        [HttpPost]

        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if(!ModelState.IsValid)
            {
                var moievDropdownsData = await _service.GetMovieDropdownsValues();
                ViewBag.Actors = new SelectList(moievDropdownsData.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(moievDropdownsData.Producers, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(moievDropdownsData.Cinemas, "Id", "Name");

                return View(movie);
            }

            await _service.AddMovieAsync(movie);
            return RedirectToAction("Index");
        }
    }
}
