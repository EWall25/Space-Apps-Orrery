#Mars:
Julian_Date = 2460439.346794
a0  =      1.52371034
da  =      0.00001847
e0  =      0.09336511
de  =      0.00009149
I0  =      1.85181869
dI  =     -0.00724757
L0  =     -4.56813164
dL  =  19140.29934243
ϖ0  =    -23.91744784
dϖ  =      0.45223625
Ω0  =     49.71320984
dΩ  =     -0.26852431


def T(Teph):
    T=((Teph-2451545.0)/36525)
    return T
T=T(Julian_Date)


a=a0 + da*T
e=e0 + de*T
I=I0 + dI*T
L=L0 + dL*T
ϖ=ϖ0 + dϖ*T
Ω=Ω0 + dΩ*T


def Arguement_of_Perihelion():
    ω=ϖ-Ω
    return ω
ω=Arguement_of_Perihelion()


def Mean_Anomaly():
    M=L-ϖ
    return M
M=Mean_Anomaly()


def Modulus_Mean_Anomaly():
    Mmod=M%360
    if Mmod>=180:
        Mmod=Mmod-360
    else:
        Mmod=Mmod
    return Mmod
Mmod=Modulus_Mean_Anomaly()
