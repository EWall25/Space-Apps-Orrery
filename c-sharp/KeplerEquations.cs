using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class KeplerEquations
{
    public const double SECONDS_PER_DAY = 86400;
    public const double G = 6.67430e-11;

    public static double EccentricAnomaly(double M, double e, double a, double centralBodyMass = 1.9891e30, double timeSinceEpoch = 0.0, double tolerance = 1e-10, int maxIterations = 100)
    {
        // Calculate mean motion n
        double n = Math.Sqrt(G * centralBodyMass / Math.Pow(a, 3));

        // Calculate mean anomaly at timeSinceEpoch
        M += n * timeSinceEpoch;

        // Normalize M to be within [0, 2π]
        M %= 2 * Math.PI;

        // Initial guess for E
        double E = e < 0.8 ? M : Math.PI; // A good initial guess can depend on e

        for (int i = 0; i < maxIterations; i++)
        {
            // Calculate the value of f(E) and its derivative f'(E)
            double f_E = E - e * Math.Sin(E) - M;
            double f_prime_E = 1 - e * Math.Cos(E);

            // Update E using Newton's method
            double E_new = E - f_E / f_prime_E;

            // Check for convergence
            if (Math.Abs(E_new - E) < tolerance)
            {
                return E_new;
            }

            E = E_new;
        }

        // If we reach here, we didn't converge
        throw new InvalidOperationException("Eccentric anomaly did not converge");
    }
}
