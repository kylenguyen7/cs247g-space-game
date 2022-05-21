using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRegionController : MonoBehaviour {
    [SerializeField] private Planet myPlanet;
    public Planet Planet => myPlanet;
}

public enum Planet {
    NONE,
    VALERIAN,
    CECARRO,
    DRORIA,
    SELAVIS,
    GAIA,
}