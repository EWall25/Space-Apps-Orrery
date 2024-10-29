import math
import time

import numpy as np

from kepler_equation import eccentric_anomaly, modulus, G
from planetary_elements import a, e, ω, Ω, I

# M = math.radians(modulus(355.43))  # Mean anomaly in radians

# J2000 ecliptic plane reference:
# x_ecl = ((np.cos(np.deg2rad(ω)))*(np.cos(np.deg2rad(Ω))))-()

# x_ecl=((((np.cos(np.deg2rad(ω)))*(np.cos(np.deg2rad(Ω))))-(((np.sin(np.deg2rad(ω))))*(np.sin(np.deg2rad(Ω)))*(np.cos(np.deg2rad(I))))*x_orbit) - ((((np.sin(np.deg2rad(ω))))*(np.cos(np.deg2rad(Ω)))+((np.cos(np.deg2rad(ω)))*(np.sin(np.deg2rad(Ω)))*(np.cos(np.deg2rad(I)))))*y_orbit))
# y_ecl=(((np.cos(np.deg2rad(ω)))*(np.sin(np.deg2rad(Ω))))-(((np.sin(np.deg2rad(ω))))*(np.cos(np.deg2rad(Ω)))*(np.cos(np.deg2rad(I))))*x_orbit - ((((np.sin(np.deg2rad(ω))))*(np.sin(np.deg2rad(Ω)))-((np.cos(np.deg2rad(ω)))*(np.cos(np.deg2rad(Ω)))*(np.cos(np.deg2rad(I)))))*y_orbit))
# z_ecl=((((((np.sin(np.deg2rad(ω)))))*(np.sin(np.deg2rad(I))))*x_orbit + (((np.cos(np.deg2rad(ω)))*(np.sin(np.deg2rad(I))))*y_orbit)))


def orbital_coordinates(time_since_epoch):
    M = math.radians(355.43)  # Mean anomaly in radians
    E = eccentric_anomaly(M, e, a * 1.495978707e11, time_since_epoch=time_since_epoch)  # Eccentric anomaly in radians

    # Orbital plane reference:
    x_orbit = a * (math.cos(E) - e)
    y_orbit = a * math.sqrt(1 - e ** 2) * math.sin(E)
    z_orbit = 0

    return x_orbit, y_orbit, z_orbit


def ecliptic_coordinates(time_since_epoch):
    x_orbit, y_orbit, z_orbit = orbital_coordinates(time_since_epoch)

    # J2000 ecliptic plane reference:
    x_ecl = ((((np.cos(np.deg2rad(ω))) * (np.cos(np.deg2rad(Ω)))) - (
                ((np.sin(np.deg2rad(ω)))) * (np.sin(np.deg2rad(Ω))) * (np.cos(np.deg2rad(I)))) * x_orbit) - ((((
        np.sin(np.deg2rad(ω)))) * (np.cos(np.deg2rad(Ω))) + ((np.cos(np.deg2rad(ω))) * (np.sin(np.deg2rad(Ω))) * (
        np.cos(np.deg2rad(I))))) * y_orbit))
    y_ecl = (((np.cos(np.deg2rad(ω))) * (np.sin(np.deg2rad(Ω)))) - (
                ((np.sin(np.deg2rad(ω)))) * (np.cos(np.deg2rad(Ω))) * (np.cos(np.deg2rad(I)))) * x_orbit - ((((
        np.sin(np.deg2rad(ω)))) * (np.sin(np.deg2rad(Ω))) - ((np.cos(np.deg2rad(ω))) * (np.cos(np.deg2rad(Ω))) * (
        np.cos(np.deg2rad(I))))) * y_orbit))
    z_ecl = ((((((np.sin(np.deg2rad(ω))))) * (np.sin(np.deg2rad(I)))) * x_orbit + (
                ((np.cos(np.deg2rad(ω))) * (np.sin(np.deg2rad(I)))) * y_orbit)))

    return x_ecl, y_ecl, z_ecl


if __name__ == "__main__":
    ref_time = time.time()
    while True:
        time_since_epoch = time.time() - ref_time
        x, y, z = ecliptic_coordinates(time_since_epoch * 86400)
        print(f"Coordinates at time T:{time_since_epoch:.2f}: X:{x:.5f} Y:{y:.5f} Z:{z:.5f}")
        time.sleep(1)
