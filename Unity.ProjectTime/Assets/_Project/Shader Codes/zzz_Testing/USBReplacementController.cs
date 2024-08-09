using UnityEngine;

namespace _Project.Shader_Codes.zzz_Testing
{
    [ExecuteInEditMode]
    public class USBReplacementController : MonoBehaviour
    {
        // replacement shader
        public Shader m_replacementShader;
    
        private void OnEnable()
        {
            if (m_replacementShader != null)
            {
                // the camera will replace all the shaders in the scene with 
                // the replacement one the “RenderType” configuration must match 
                // in both shader
                GetComponent<Camera>().SetReplacementShader(
                    m_replacementShader, "RenderType");
                Debug.Log("Enabled");
            }

            
            
        }
    
        private void OnDisable()
        {
            // let's reset the default shader
            GetComponent<Camera>().ResetReplacementShader();
            Debug.Log("Disabled");
        }
    }
} 


