using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApp.DTOs;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.BusinessLogic
{
    public class GameLogic
    {
        private readonly GameService _gameService;

        public GameLogic()
        {
            _gameService = new GameService();
        }

        public async Task<Game?> CreateGame(GameDto game)
        {
            return await _gameService.CreateGame(game);
        }

    }
}
