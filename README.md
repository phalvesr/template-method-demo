
# Template method demo

> Define o esqueleto de um algoritmo em uma operação, delegando alguns passos a subclasses. Faz com que classes mais especializadas implementem partes do algoritmo original, mas sem a capacidade de alterá-lo.

![Diagrama Template Method](/docs/img/template-method-structure.png)

## Estudo de caso "Criação/Atualização cadastral em Renda Fixa"

Considere um caso onde uma app recebe de uma fonte externa qualquer um identificador único de um cliente. Este identificador único dá inicio a um processo de criação ou atualização de um cadastro. Ao fim do processamento devemos ter o cliente salvo dentro do nosso sistema de renda fixa. Os dados presentes na sigla de origem não são totalmente compatíveis com os dados na sigla de destino, portanto, entre a recepção dos dados e o seu armazenamento devemos fazer um *mapping*. O leitor final poderá esboçar o fluxograma da operação descrita, mas a titulo de exemplo segue um: 

![Fluxograma](/doc/img/fluxograma.png)

Analisando o fluxograma a parte em destaque abaixo são dois fortes indicativos de que podemos modelar o software desta modalidade utilizando o padrão **template method**.

## Exemplo de utilização do pattern no framework xamarin
Renderização de uma lista em um aparelho mobile:
```
public class PhotoListAdapter : RecyclerView.Adapter
{
    public IList<Photo> _photos;
    
    public PhotoListAdapter(IList<Photo>photos)
    {
        _photos = photos;
    }

    public override RecyclerView.ViewHolder OnCreateViewHolder (ViewGroup parent, int viewType)
    {
        // Criação da view
    }

    public override void OnBindViewHolder (RecyclerView.ViewHolder holder, int position)
    {
        // Anexação de dados na view
    }

    public override int ItemCount => _photos.Count;
}
```

