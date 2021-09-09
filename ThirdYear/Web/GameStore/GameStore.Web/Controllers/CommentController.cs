using System.Collections.Generic;
using AutoMapper;
using GameStore.Domain.Interfaces.Services;
using GameStore.Domain.Models;
using GameStore.Web.Helpers;
using GameStore.Web.ViewModel.Comment;
using GameStore.Web.ViewModel.User;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly string _authorId;

        public CommentController(
            IGameService gameService,
            ICommentService commentService,
            IUserService userService,
            IMapper mapper)
        {
            _commentService = commentService;
            _gameService = gameService;
            _userService = userService;
            _mapper = mapper;
            _authorId = "1";
        }

        [HttpPost]
        [Route("/game/{gameKey}/newcomment")]
        public ActionResult LeaveComment(string gameKey, CommentViewModel commentViewModel)
        {
            if (AssertGameExists(gameKey))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            if (!ModelState.IsValid)
            {
                var user = _userService.GetById(_authorId);
                var userViewModel = _mapper.Map<UserViewModel>(user);
                var comments = _commentService.GetComments(gameKey);
                var commentsViewModel = new ListCommentsViewModel
                {
                    Comments = _mapper.Map<ICollection<CommentViewModel>>(comments),
                    GameKey = gameKey,
                    NewComment = commentViewModel,
                    User = userViewModel,
                };

                return View(nameof(CommentController.GetComments), commentsViewModel);
            }

            var comment = _mapper.Map<Comment>(commentViewModel);
            _commentService.AddComment(gameKey, comment);

            return Redirect($"~/game/{gameKey}/comments/false");
        }

        [HttpGet]
        [Route("/game/{gameKey}/comments/{isAdmin}")]
        [ResponseCache(Duration = 60)]
        public ActionResult GetComments(string gameKey, bool isAdmin)
        {
            if (AssertGameExists(gameKey))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var comments = _commentService.GetComments(gameKey);

            if (comments is null)
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var user = _userService.GetById(_authorId);
            var userViewModel = _mapper.Map<UserViewModel>(user);

            var game = _gameService.GetByKey(gameKey);

            var commentsViewModel = new ListCommentsViewModel
            {
                Comments = _mapper.Map<ICollection<CommentViewModel>>(comments),
                GameKey = game.Key,
                NewComment = new CommentViewModel(),
                User = userViewModel,
                IsAdmin = isAdmin,
            };

            return View(commentsViewModel);
        }

        [HttpPost]
        [Route("/game/{gameKey}/comment/delete/{commentId}")]
        public ActionResult Delete(string gameKey, string commentId)
        {
            if (!_commentService.IsExist(commentId) || AssertGameExists(gameKey))
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            var comments = _commentService.GetComments(gameKey);

            if (comments is null)
            {
                return RedirectToAction(ResourceNames.Methods.NotFound, ResourceNames.Classes.Error);
            }

            _commentService.Delete(commentId);

            var game = _gameService.GetByKey(gameKey);
            var user = _userService.GetById(_authorId);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            var commentsViewModel = new ListCommentsViewModel
            {
                Comments = _mapper.Map<ICollection<CommentViewModel>>(comments),
                GameKey = game.Key,
                NewComment = new CommentViewModel(),
                User = userViewModel,
                IsAdmin = true,
            };

            return View(nameof(CommentController.GetComments), commentsViewModel);
        }

        private bool AssertGameExists(string gameKey)
        {
            return string.IsNullOrEmpty(gameKey) || !_gameService.IsExist(gameKey);
        }
    }
}
