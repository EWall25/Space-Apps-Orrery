#Heliocentric Coordinates:
import numpy as np
from planetary_elements import a, e
E = 0
Xcent = a * (np.cos(np.deg2rad(E))-e)
Ycent = a * np.sqrt(1-(e**2))*np.sin(np.deg2rad(E))
Zcent = 0
print(Xcent)
print(Ycent)
print(Zcent)
Xecl=((np.cos(np.deg2rad(ω)))*(np.cos(np.deg2rad(Ω))))-()