using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Diagnostics;

[RequireComponent(typeof(PlanetaryElements))]
[RequireComponent(typeof(TrailRenderer))]
public class PlanetaryCoordinates : MonoBehaviour
{
    public float timeScale = 1;
    public float obritSizeScale = 1;


    PlanetaryElements el;
    TrailRenderer trail;
    public double T_orb;
    float timeAlive = 0f;

    Vector3 OrbitalCoordinates(double timeSinceEpoch)
    {
        double M = DegreesToRadians(355.43);  // Mean anomaly in radians
        double E = KeplerEquations.EccentricAnomaly(M, el.e, el.a * 1.495978707e11, timeSinceEpoch: timeSinceEpoch);  // Eccentric anomaly in radians

        // Orbital plane reference:
        double x_orbit = el.a * (Math.Cos(E) - el.e);
        double y_orbit = el.a * Math.Sqrt(1 - el.e * el.e) * Math.Sin(E);
        double z_orbit = 0;

        return new Vector3((float) x_orbit, (float) y_orbit, (float) z_orbit);
    }

    Vector3 EclipticCoordinates(double timeSinceEpoch)
    {
        var coordinates = OrbitalCoordinates(timeSinceEpoch);
        float x_orbit = coordinates.x;
        float y_orbit = coordinates.y;

        // Convert degrees to radians for angles
        double ω_rad = DegreesToRadians(el.ω);
        double Ω_rad = DegreesToRadians(el.Ω);
        double I_rad = DegreesToRadians(el.I);

        // J2000 ecliptic plane reference
        double x_ecl = (Math.Cos(ω_rad) * Math.Cos(Ω_rad) - 
                        (Math.Sin(ω_rad) * Math.Sin(Ω_rad) * Math.Cos(I_rad) * x_orbit) - 
                        (Math.Sin(ω_rad) * Math.Cos(Ω_rad) + 
                        (Math.Cos(ω_rad) * Math.Sin(Ω_rad) * Math.Cos(I_rad)) * y_orbit));

        double y_ecl = (Math.Cos(ω_rad) * Math.Sin(Ω_rad) - 
                        (Math.Sin(ω_rad) * Math.Cos(Ω_rad) * Math.Cos(I_rad) * x_orbit) - 
                        (Math.Sin(ω_rad) * Math.Sin(Ω_rad) - 
                        (Math.Cos(ω_rad) * Math.Cos(Ω_rad) * Math.Cos(I_rad) * y_orbit)));

        double z_ecl = (Math.Sin(ω_rad) * Math.Sin(I_rad) * x_orbit + 
                        (Math.Cos(ω_rad) * Math.Sin(I_rad) * y_orbit));

        return new Vector3((float) x_ecl, (float) y_ecl, (float) z_ecl);
    }

    Vector3 RotateOrbit(Vector3 orbit)
    {
        var rotation = Quaternion.Euler((float) el.ω, (float) el.I, (float) el.Ω);
        return rotation * orbit;
    }

    static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180.0);
    }

    // Start is called before the first frame update
    void Start()
    {
        el = GetComponent<PlanetaryElements>();
        trail = GetComponent<TrailRenderer>();
        trail.time = 0;
        T_orb = 2 * Math.PI * Math.Sqrt(Math.Pow(el.a * 1.495978707e11, 3) / (KeplerEquations.G * 1.9891e30));
    }

    // Update is called once per frame
    void Update()
    {
        timeAlive += Time.deltaTime;

        Vector3 xyz;
        try {
            xyz = OrbitalCoordinates(Time.time * KeplerEquations.SECONDS_PER_DAY * timeScale);
        } catch (InvalidOperationException) {
            
            UnityEngine.Debug.LogWarning($"Eccentric anomaly did not converge for small body {gameObject.name}, so the GameObject will be destroyed.");
            Destroy(gameObject);
            return;
        }
        transform.position = RotateOrbit(xyz) * obritSizeScale;

        if (timeAlive > 0.5f) {
            T_orb = 2 * Math.PI * Math.Sqrt(Math.Pow(el.a * 1.495978707e11, 3) / (KeplerEquations.G * 1.9891e30));
            trail.time = (float) (T_orb / KeplerEquations.SECONDS_PER_DAY / timeScale);
        }
    }
}
