using System;
using System.Collections.Generic;
using TriInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class EnemyArea : MonoBehaviour
    {
        public List<Enemy> Prefabs;
        [SerializeField]
        private PolygonCollider2D poly;
        
        private void Awake()
        {
            poly = GetComponent<PolygonCollider2D>();
        }

        public Vector2 GetRandomPosition()
        {
            var bounds = poly.bounds;
            Vector2 from = bounds.min;
            Vector2 to = bounds.max;

            Vector2 p = GenRandom(from, to);
            while (!poly.OverlapPoint(p))
            {
                p = GenRandom(from, to);
            }

            return p;
        }

        [Button]
        public void SpawnEnemy()
        {
            var prefab = Prefabs[Random.Range(0, Prefabs.Count)];
            var go = Instantiate(prefab, GetRandomPosition(), Quaternion.identity, transform);
            go.area = this;
        }

        private Vector2 GenRandom(Vector2 min, Vector2 max)
        {
            return new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        }
    }
}