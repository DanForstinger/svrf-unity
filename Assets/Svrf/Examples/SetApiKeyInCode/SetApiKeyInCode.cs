﻿using System.Linq;
using Svrf.Models.Http;
using UnityEngine;

namespace Svrf.Unity.Examples
{
    public class SetApiKeyInCode : MonoBehaviour
    {
        private static SvrfApi _svrfApi;

        async void Start()
        {
            SvrfApiKey.Value = "your key";

            _svrfApi = new SvrfApi();

            var trendingResponse = await _svrfApi.Media.GetTrendingAsync(new MediaRequestParams { IsFaceFilter = true });
            var model = trendingResponse.Media.First();

            var svrfModel = await SvrfModel.GetSvrfModel(model);
            svrfModel.transform.SetParent(transform);
        }
    }
}