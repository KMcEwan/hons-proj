using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class urpUsed : MonoBehaviour
{
    public RenderPipelineAsset smallScreenPipeline;
    void Start()
    {
        //GraphicsSettings.defaultRenderPipeline = smallScreenPipeline;
        QualitySettings.renderPipeline = smallScreenPipeline;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
