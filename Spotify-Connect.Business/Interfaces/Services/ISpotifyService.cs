﻿using Spotify_Connect.Business.Dto.Client.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify_Connect.Business.Interfaces.Services
{
	public interface ISpotifyService
	{
		Task<SpotifyGetAccessTokenDto> GetAccessToken();

	}
	}
