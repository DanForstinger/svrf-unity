﻿using Assets.Scripts.Models;
using Svrf.Models.Media;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityGLTF;

namespace Assets.Scripts.Utilities
{
    internal static class SvrfModelUtility
    {
        internal static async Task AddSvrfModel(GameObject gameObject, MediaModel model, SvrfModelOptions options)
        {
            var gltfComponent = gameObject.AddComponent<GLTFComponent>();
            gltfComponent.GLTFUri = model.GetMainGltfFile();

            var shader = options.ShaderOverride == null ? Shader.Find("Standard") : options.ShaderOverride;

            var gltfShaderField = gltfComponent
                .GetType()
                .GetField("shaderOverride", BindingFlags.NonPublic | BindingFlags.Instance);
            gltfShaderField.SetValue(gltfComponent, shader);

            await gltfComponent.Load();

            var gltfRoot = gameObject.transform.Find("Root Scene");
            var rootNode = gltfRoot.Find("RootNode");
            var occluder = rootNode.Find("Occluder");

            gltfRoot.transform.Rotate(Vector3.up, 180);

            if (occluder == null)
            {
                return;
            }

            if (options.WithOccluder)
            {
                var meshRenderer = occluder.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
                meshRenderer.sharedMaterials[0].shader = Shader.Find("Custom/OccluderShader");
            }
            else
            {
                occluder.gameObject.SetActive(false);
            }
        }
    }
}
