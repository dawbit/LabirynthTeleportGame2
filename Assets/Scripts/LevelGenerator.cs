using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    public ColorToPrefarb[] collorMappings;
    public float offset = 5f;

    public Material material01;
    public Material material02;

    void GenerateTile(int x, int z)
    {
        //Pobieramy kolor pixela w pozycji x i z
        Color pixelColor = map.GetPixel(x, z);

        //Jeżeli alpha koloru jest równa 0, czyli kolor jest w pełni przeźroczysty to pomijamy pixel
        if(pixelColor.a == 0) 
        {
            return;
        } 

        //Przeszukiwanie po wszystkich kolorach w naszej tablicy
        foreach(ColorToPrefarb collorMapping in collorMappings)
        {
            //Jeżeli któryś z kolorów w naszej tablicy odpowiada to ustaw prefarb
            if(collorMapping.color.Equals(pixelColor))
            {
                //wylicz pozycje na podstawie współrzędnej pixela
                Vector3 position = new Vector3(x, 0, z) * offset;
                // Utwórz wybrany obiekt w wybranej pozycji
                Instantiate(collorMapping.prefarb, position, Quaternion.identity, transform);
            }
        }
    }

    public void GenerateLabirynth()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int z = 0; z < map.height; z++)
            {
                GenerateTile(x,z);
            }
        }

        ColorTheChildren();
    }

    public void ColorTheChildren()
    {
        foreach(Transform child in transform) 
        {
            if (child.tag == "Wall") 
            {
                if (Random.Range(1, 100) % 3 == 0) 
                {
                    child.gameObject.GetComponent<Renderer>().material = material02;
                }
                else {
                    child.gameObject.GetComponent<Renderer>().material = material01;
                }
            }

            //W tym miejscu można dodać kawałek kodu, który będzie sprawdzał czy ściana ma jeszcze jakieś dzieci i by im
            //też nadała materiały w ten sposób

            if(child.childCount > 0)
            {
                foreach(Transform grandchild in child.transform)
                {
                    if (grandchild.tag == "Wall") 
                    {
                        if(Random.Range(1, 100) % 3 == 0) 
                        {
                            grandchild.gameObject.GetComponent<Renderer>().material = material02;
                        }
                        else {
                            grandchild.gameObject.GetComponent<Renderer>().material = material01;
                        }
                    }
                }
            }
        }
    }
}
