using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using System;

public class SmallBodyLoader : MonoBehaviour
{
    public float sizeScale = 0.05f;
    public float obritSizeScale = 1;
    public float timeScale = 100;
    public float trailWidth = 0.1f;

    void Awake()
    {
        // Load small bodies from CSV
        var fileList = new List<TextAsset> {
            Resources.Load<TextAsset>("smallBodies"),
            Resources.Load<TextAsset>("planets"),
        };
        var dataDictionary = new Dictionary<string, Dictionary<string, string>>();

        foreach (TextAsset csvFile in fileList)
        {
            using (var csvReader = new CsvReader(new StreamReader(new MemoryStream(csvFile.bytes)), true))
            {
                // Get the headers
                string[] headers = csvReader.GetFieldHeaders();

                while (csvReader.ReadNextRecord())
                {
                    // Use the first field (spkid) as the key
                    string spkid = csvReader[0];
                    var rowDictionary = new Dictionary<string, string>();

                    for (int i = 0; i < headers.Length; i++)
                    {
                        rowDictionary[headers[i]] = csvReader[i];
                    }

                    dataDictionary[spkid] = rowDictionary;
                }
            }
        }

        Material lineMaterial = new Material(Shader.Find("Sprites/Default"));

        // Generate small bodies from data dict
        foreach (var kvp in dataDictionary)
        {
            Dictionary<string, string> keplerParams = kvp.Value;

            // Accessing and converting values
            string name = keplerParams["full_name"];
            double e = Convert.ToDouble(keplerParams["e"]);
            double a = Convert.ToDouble(keplerParams["a"]);
            double i = Convert.ToDouble(keplerParams["i"]);
            double om = Convert.ToDouble(keplerParams["om"]);
            double w = Convert.ToDouble(keplerParams["w"]);
            double ma = Convert.ToDouble(keplerParams["ma"]);

            // Diameter might null
            double diameter;
            try {
                diameter = Convert.ToDouble(keplerParams["diameter"]);
            } catch {
                diameter = 1;
            }

            float radius = (float) diameter / 2;
            radius *= sizeScale;

            // Create the sphere
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            // Set the sphere's scale (optional)
            sphere.transform.localScale = new Vector3(radius, radius, radius);

            Gradient gradient = new Gradient();
            Color randomColor = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);

            // Create color keys for the gradient
            GradientColorKey[] colorKeys = new GradientColorKey[2];
            colorKeys[0] = new GradientColorKey(randomColor, 0.0f); // Start color
            colorKeys[1] = new GradientColorKey(randomColor, 1.0f); // End color

            // Create alpha keys for the gradient (fully opaque)
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(1.0f, 0.0f); // Start alpha
            alphaKeys[1] = new GradientAlphaKey(1.0f, 1.0f); // End alpha

            // Set the keys to the gradient
            gradient.colorKeys = colorKeys;
            gradient.alphaKeys = alphaKeys;

            // Add and configure the TrailRenderer
            TrailRenderer trailRenderer = sphere.AddComponent<TrailRenderer>();
            trailRenderer.startWidth = trailWidth;
            trailRenderer.endWidth = trailRenderer.startWidth;
            trailRenderer.material = lineMaterial;
            trailRenderer.colorGradient = gradient;
            trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            trailRenderer.receiveShadows = false;

            // Remove the Sphere Collider
            // Destroy(sphere.GetComponent<SphereCollider>());
            
            // Optionally, you can add a movement script here
            PlanetaryCoordinates pc = sphere.AddComponent<PlanetaryCoordinates>();
            pc.timeScale = timeScale;
            pc.obritSizeScale = obritSizeScale;

            // Update planetary elements
            PlanetaryElements el = sphere.GetComponent<PlanetaryElements>();
            el.e = e;
            el.a = a;
            el.I = i;
            el.Ω = om;
            el.ω = w;
            el.M = ma;

            sphere.name = name;

            // Set small body material
            MeshRenderer renderer = sphere.GetComponent<MeshRenderer>();
            renderer.material = Resources.Load<Material>("PlanetMaterial");

            sphere.AddComponent<TrailRendererColliderGenerator>();
        }
    }

    void Update()
    {
        
    }
}
