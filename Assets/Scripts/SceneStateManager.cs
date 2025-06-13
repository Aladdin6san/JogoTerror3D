using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStateManager : MonoBehaviour
{
    [System.Serializable]
    public class ObjectState
    {
        public Vector3 posicao;
        public bool ativo;
    }

    [System.Serializable]
    public class EstadoDaCena
    {
        public int contadorItens;
        public int municaoJogador;
        public List<ObjectState> objetos = new List<ObjectState>();
    }

    public List<GameObject> objetosParaSalvar; // Inclui balas, itens, inimigos, jogador etc.
    public Tiro tiroInstance; // Referência ao script Tiro no jogador

    private string caminhoArquivo;

    private bool jaSalvou = false;


    void Start()
    {
        caminhoArquivo = Application.persistentDataPath + "/save.json";
        CarregarEstado();
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.P))
        {
            ResetarSave();
            return; // evita executar o restante
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SalvarEstado();
            return;
        }*/

        // Salvar apenas uma vez ao alcançar o sexto item
        if (Item.ObterContador() == 6 && !jaSalvou)
        {
            SalvarEstado();
            jaSalvou = true;
        }
    }

    public void ResetarSave()
    {
        if (File.Exists(caminhoArquivo))
        {
            File.Delete(caminhoArquivo);
            Debug.Log("Save resetado! Arquivo JSON deletado.");
        }

        Item.ResetarItens(); // reseta o contador para 0
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // recarrega a cena
    }


    public void SalvarEstado()
    {
        EstadoDaCena estado = new EstadoDaCena();
        estado.contadorItens = Item.ObterContador();
        estado.municaoJogador = tiroInstance.ObterMunicao();

        foreach (GameObject obj in objetosParaSalvar)
        {
            ObjectState estadoObjeto = new ObjectState
            {
                posicao = obj.transform.position,
                ativo = obj.activeSelf
            };
            estado.objetos.Add(estadoObjeto);
        }

        string json = JsonUtility.ToJson(estado, true);
        File.WriteAllText(caminhoArquivo, json);
        Debug.Log("Estado salvo em: " + caminhoArquivo);
    }

    public void CarregarEstado()
    {
        if (File.Exists(caminhoArquivo))
        {
            string json = File.ReadAllText(caminhoArquivo);
            EstadoDaCena estado = JsonUtility.FromJson<EstadoDaCena>(json);

            Item.DefinirContador(estado.contadorItens);
            tiroInstance.DefinirMunicao(estado.municaoJogador);

            // Atualiza texto no UI após carregar o contador
            foreach (GameObject obj in objetosParaSalvar)
            {
                Item itemScript = obj.GetComponent<Item>();
                if (itemScript != null)
                {
                    itemScript.SendMessage("AtualizarTexto");
                    break; // só precisa de um para atualizar o texto
                }
            }

            for (int i = 0; i < estado.objetos.Count && i < objetosParaSalvar.Count; i++)
            {
                GameObject obj = objetosParaSalvar[i];
                ObjectState estadoObj = estado.objetos[i];

                obj.transform.position = estadoObj.posicao;
                obj.SetActive(estadoObj.ativo);

                Debug.Log($"[Load] Objeto {obj.name} restaurado: ativo = {estadoObj.ativo}");
            }

            Debug.Log("Estado carregado!");
        }
    }
}
