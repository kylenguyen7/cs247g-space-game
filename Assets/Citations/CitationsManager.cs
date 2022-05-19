using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitationsManager : MonoBehaviour {
    public static CitationsManager Instance;
    [SerializeField] private GameObject citationPrefab;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private int _numCitations;
    public int NumCitations => _numCitations;

    public void CreateCitation(bool packageWasDelivered) {
        _numCitations++;
        var citation = Instantiate(citationPrefab, Vector2.zero, Quaternion.identity)
            .GetComponent<CitationController>();
        citation.Init(packageWasDelivered);
    }
}
