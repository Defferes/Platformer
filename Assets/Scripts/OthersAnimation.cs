using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OthersAnimation : MonoBehaviour
{
    [SerializeField] private int framesPerSecond;
    [SerializeField] private bool isCycle;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private UnityEvent _OnComplite;

    private SpriteRenderer _spriteRenderer;
    private int iteration;
    private float timeBetweenFrames, nextFrameTime;
    private bool isPlaying = true;

    private void Start()
    {
        timeBetweenFrames = 1f / framesPerSecond;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        nextFrameTime = Time.time + timeBetweenFrames;
    }

    private void Update()
    {
        if (isPlaying && nextFrameTime > Time.time) return;
        
        if (iteration == _sprites.Length)
        {
            if (isCycle)
            {
                iteration = 0;
            }
            else
            {
                isPlaying = false;
                _OnComplite?.Invoke();
                return;
            }
        }
        _spriteRenderer.sprite = _sprites[iteration];
        nextFrameTime += timeBetweenFrames;
        iteration++;
        
    }
}
