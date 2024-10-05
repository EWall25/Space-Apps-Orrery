import numpy as np

from planetary_elements import Mmod, e


def modulus(mean_anomaly):
    M_mod = mean_anomaly % 360
    if M_mod >= 180:
        M_mod = M_mod - 360
    return M_mod


def iterate(E, mean_anomaly, eccentricity):
    eccentricity_deg = np.rad2deg(eccentricity)
    E_rad = np.deg2rad(E)

    delta_M = mean_anomaly - (E - eccentricity_deg * np.sin(E_rad))
    delta_E = delta_M / (1 - eccentricity * np.cos(E_rad))
    return delta_E


def calc_E(mean_anomaly, eccentricity, tolerance=1e-6):
    eccentricity_deg = np.rad2deg(eccentricity)
    mean_anomaly_rad = np.deg2rad(mean_anomaly)

    E = mean_anomaly  # mean_anomaly + eccentricity_deg * np.sin(mean_anomaly_rad)
    i = 0
    while True:
        delta_E = iterate(E, mean_anomaly, eccentricity)
        if abs(delta_E) < tolerance:
            print(f"Iterations: {i}")
            break
        E += delta_E
        i += 1
    return E


if __name__ == '__main__':
    E = calc_E(modulus(355.43), 0.09339410)
    print(E)
