using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
        //----------------------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> FilteredMovies(string searchString)
        {
            var allmovies = await _service.GetAllAsync(o => o.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allmovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) 
                                                       || n.Description.ToLower().Contains(searchString.ToLower()))
                                                         .ToList();
                if (filteredResult.IsNullOrEmpty())
                {
                    return View("NotFound");
                }

                return View("Index",filteredResult);
            };

            return View("Index",allmovies);
        }
        /// <summary>
        /// here i am using "PRG (Post Redirect Get)" method for preventing refreshe or reload after submit form   
        /// </summary>
        /// <param name="searchString"> Passing data to Get Moethd</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Filter(string searchString)
        {
            return RedirectToAction("FilteredMovies", new { searchString });
        }
        //------------------------------------------------------------------------------------------------------------
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

        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync(id);
            if(movieDetail == null) return View("NotFound");

            var response = new NewMovieVM()
            {
                Id = movieDetail.Id,
                Name = movieDetail.Name,
                Description = movieDetail.Description,
                Price = movieDetail.Price,
                ImageURL = movieDetail.ImageURL,
                MovieCategory = movieDetail.MovieCategory,
                CinemaId = movieDetail.CinemaId,
                ProducerId = movieDetail.ProducerId,
                StartDate= movieDetail.StartDate,
                EndDate= movieDetail.EndDate,
                ActorIds = movieDetail.Actor_Movies.Select(n => n.ActorId).ToList(),
            };

            var moievDropdownsData = await _service.GetMovieDropdownsValues();

            ViewBag.Actors = new SelectList(moievDropdownsData.Actors, "Id", "FullName");
            ViewBag.Producers = new SelectList(moievDropdownsData.Producers, "Id", "FullName");
            ViewBag.Cinemas = new SelectList(moievDropdownsData.Cinemas, "Id", "Name");

            return View(response);
        }

        //POST : Movie/Edit
        [HttpPost]

        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if (id != movie.Id) return View("NotFound");


            if (!ModelState.IsValid)
            {
                var moievDropdownsData = await _service.GetMovieDropdownsValues();
                ViewBag.Actors = new SelectList(moievDropdownsData.Actors, "Id", "FullName");
                ViewBag.Producers = new SelectList(moievDropdownsData.Producers, "Id", "FullName");
                ViewBag.Cinemas = new SelectList(moievDropdownsData.Cinemas, "Id", "Name");

                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction("Index");
        }
    }
}
