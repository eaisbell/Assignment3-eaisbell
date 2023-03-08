using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment3_V2.Data;
using Assignment3_V2.Models;
using Tweetinvi;
using VaderSharp2;
using Assignment3_V2.Data;
using Assignment3_V2.Models;

namespace Assignment3_V2.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetActorPhoto(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }
            var imageData = actor.ActorPhoto;
            return File(imageData, "image/jpg");
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Actors.Include(a => a.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }

            ActorDetailsVM actorDetailsVM = new ActorDetailsVM();
            actorDetailsVM.actor = actors;
            actorDetailsVM.Tweets = new List<ActorTweet>();

            var userClient = new TwitterClient("AAx9UfdCemph0Pg0t8Moq5c6L", "LbhoERpFGjBESYSNjTHuRvE0R80cGxZBx5lJWanM5lFpO2Hs63", "1455230009153503238-WTxQgoYUAQ3D9PTSsUu8stHkmJvuVe", "2ZVnM9tWbCSNAhyJcyC4WPIgiIbUWZ77MTLSx2Qb8TkW3");
            var searchResponse = await userClient.SearchV2.SearchTweetsAsync(actors.Name);
            var tweets = searchResponse.Tweets;
            var analyzer = new SentimentIntensityAnalyzer();
            foreach (var tweetText in tweets)
            {
                var tweet = new ActorTweet();
                tweet.TweetText = tweetText.Text;
                var results = analyzer.PolarityScores(tweet.TweetText);
                tweet.Sentiment = results.Compound;
                actorDetailsVM.Tweets.Add(tweet);
            }

            return View(actorDetailsVM);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Gender,Age,IMBDLink,MovieId")] Actors actors, IFormFile ActorPhoto)
        {
            if (ModelState.IsValid)
            {
                if (ActorPhoto != null && ActorPhoto.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    await ActorPhoto.CopyToAsync(memoryStream);
                    actors.ActorPhoto = memoryStream.ToArray();
                }
                _context.Add(actors);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Title", actors.MovieId);
            return View(actors);
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors.FindAsync(id);
            if (actors == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", actors.MovieId);
            return View(actors);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Gender,Age,IMBDLink,ActorPhoto,MovieId")] Actors actors)
        {
            if (id != actors.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actors);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorsExists(actors.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", actors.MovieId);
            return View(actors);
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Actors == null)
            {
                return NotFound();
            }

            var actors = await _context.Actors
                .Include(a => a.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actors == null)
            {
                return NotFound();
            }

            return View(actors);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Actors == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Actors'  is null.");
            }
            var actors = await _context.Actors.FindAsync(id);
            if (actors != null)
            {
                _context.Actors.Remove(actors);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActorsExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
