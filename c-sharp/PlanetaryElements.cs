using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetaryElements : MonoBehaviour
{
    /*
    [Header("Initial Attributes")]
    [SerializeField] private double Julian_Date = 2460439.346794;
    [SerializeField] private double a0 = 1.52371034;
    [SerializeField] private double da = 0.00001847;
    [SerializeField] private double e0 = 0.09336511;
    [SerializeField] private double de = 0.00009149;
    [SerializeField] private double I0 = 1.85181869;
    [SerializeField] private double dI = -0.00724757;
    [SerializeField] private double L0 = -4.56813164;
    [SerializeField] private double dL = 19140.29934243;
    [SerializeField] private double ϖ0 = -23.91744784;
    [SerializeField] private double dϖ = 0.45223625;
    [SerializeField] private double Ω0 = 49.71320984;
    [SerializeField] private double dΩ = -0.26852431;
*/

    [Header("Runtime Attributes")]
    // public double T;
    public double ω; // Argument of perihelion
    public double M; // Mean anomaly
    public double a;
    public double e;
    public double I;
    // public double L;
    // public double ϖ;
    public double Ω;

    void Start()
    {
        /*
        T = (Julian_Date - 2451545.0) / 36525;
        a = a0 + da * T;
        e = e0 + de * T;
        I = I0 + dI * T;
        L = L0 + dL * T;
        ϖ = ϖ0 + dϖ * T;
        Ω = Ω0 + dΩ * T;
        ω = ϖ - Ω;
        M = L - ϖ;
        */
    }
}
