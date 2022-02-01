#version 330 core

struct Material {
    sampler2D diffuse;
    sampler2D specular;
    float     shininess;
};

struct Light {
    //For a directional light we dont need the lights position to calculate the direction.
    //Since the direction is the same no matter the position of the fragment we also dont need that.
    vec3 direction;

    vec3 ambient;
    vec3 diffuse;
    vec3 specular;
};

uniform vec3 noTextureColor;
uniform bool useTexture;

uniform Light light;
uniform Material material;
uniform vec3 viewPos;

out vec4 FragColor;

in vec3 Normal;
in vec2 TexCoords;

vec4 GetTextured()
{
     // ambient
     vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
 
     // diffuse 
     vec3 norm = normalize(Normal);
     vec3 lightDir = normalize(-light.direction);    //We still normalize the light direction since we techically dont know,
                                                     //wether it was normalized for us or not.
     float diff = max(dot(norm, lightDir), 0.0);
     vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
 
     // specular
     vec3 viewDir = normalize(viewPos);
     vec3 reflectDir = reflect(-lightDir, norm);
     float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
     vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
 
     vec3 finalColor = ambient + diffuse + specular;
     vec4 result = vec4(finalColor, 1.0);
     return result;
}

vec4 GetUnTextured()
{
     // ambient
     vec3 ambient = light.ambient * noTextureColor;
 
     // diffuse 
     vec3 norm = normalize(Normal);
     vec3 lightDir = normalize(-light.direction);    //We still normalize the light direction since we techically dont know,
                                                     //wether it was normalized for us or not.
     float diff = max(dot(norm, lightDir), 0.0);
     vec3 diffuse = light.diffuse * diff  * noTextureColor;
 
     // specular
     vec3 viewDir = normalize(viewPos);
     vec3 reflectDir = reflect(-lightDir, norm);
     float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
     vec3 specular = light.specular * spec  * noTextureColor;
 
     vec3 finalColor = ambient + diffuse + specular;
     vec4 result = vec4(finalColor, 1.0);
     return result;
}


void main()
{
    if (useTexture)
    {
        FragColor = GetTextured();
        return;
    }
    FragColor = GetUnTextured();
}

// Textured
// void main()
// {
//     // ambient
//     vec3 ambient = light.ambient * vec3(texture(material.diffuse, TexCoords));
// 
//     // diffuse 
//     vec3 norm = normalize(Normal);
//     vec3 lightDir = normalize(-light.direction);    //We still normalize the light direction since we techically dont know,
//                                                     //wether it was normalized for us or not.
//     float diff = max(dot(norm, lightDir), 0.0);
//     vec3 diffuse = light.diffuse * diff * vec3(texture(material.diffuse, TexCoords));
// 
//     // specular
//     vec3 viewDir = normalize(viewPos);
//     vec3 reflectDir = reflect(-lightDir, norm);
//     float spec = pow(max(dot(viewDir, reflectDir), 0.0), material.shininess);
//     vec3 specular = light.specular * spec * vec3(texture(material.specular, TexCoords));
// 
//     vec3 result = ambient + diffuse + specular;
//     FragColor = vec4(result, 1.0);
// }