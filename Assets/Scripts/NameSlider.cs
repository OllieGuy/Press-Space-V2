using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameSlider : MonoBehaviour
{
    string[] names = {"Buford Tinkleton", "Gertrude McSnufflepants", "Mortimer Fizzlebottom", "Prudence Puddlefuss", "Archibald Wobblefink", "Hortense McFlapdoodle", "Reginald Pipsqueak", "Millicent Crumplehorn", "Cedric Noodlebop", "Ethelbert Fiddlesticks", "Winifred Pumpernickel", "Elmer Floopernoodle", "Petunia Cacklefeather", "Percival Blubberwhip", "Gertrude Gobbledygook", "Cornelius Snorkelsnout", "Mabel Wobblewhiskers", "Egbert Snickerdoodle", "Priscilla Bumblebump", "Algernon Wifflebottom", "Penelope Fluffernutter", "Reginald Fuzzybritches", "Beatrice Quibblepants", "Mortimer Giggletush", "Tabitha Bunsenburner", "Barnaby Quirkysnoot", "Wilhelmina Crumpetwiggle", "Dudley McSquiggle", "Esmeralda Wobblewump", "Horace Noodlenoggin", "Jemima Tiddlywinks", "Percival Fuzzywiggle", "Agatha Whippersnapper", "Archibald Doodlepants", "Millicent Bumblebop", "Mortimer Biscuitfluff", "Cornelius Puddingpants", "Prudence Quirkysniff", "Egbert Wobbleflop", "Gertrude Fluffernoodle", "Buford Pipsqueak", "Hortense McFiddlesticks", "Elmer Bumblegum", "Winifred Crumplehorn", "Cedric Quibblefuss", "Ethelbert Snorkelsnout", "Percival Noodlebottom", "Petunia Fuzzysnoot", "Reginald McFlapdoodle", "Mabel Crumpetwiggle", "Algernon Pumpernickel", "Penelope Wobblewhiskers", "Beatrice Floopernoodle", "Cornelius Gobbledygook", "Priscilla Quirkypants", "Barnaby Tinkleton", "Esmeralda Giggletush", "Horace Fiddlesticks", "Jemima Bumblebump", "Wilhelmina Noodlebop", "Dudley Snickerdoodle", "Gertrude Fuzzywiggle", "Cornelius Doodlepants", "Prudence Bumblebop", "Egbert Biscuitfluff", "Hortense Puddingpants", "Winifred Quirkysnoot", "Cedric Wobbleflop", "Ethelbert Fluffernoodle", "Percival Pipsqueak", "Petunia McFiddlesticks", "Reginald Bumblegum", "Mabel Crumpetwiggle", "Algernon Quibblefuss", "Penelope Snorkelsnout", "Beatrice Noodlebottom", "Cornelius Fuzzysnoot", "Priscilla McFlapdoodle", "Barnaby Crumpetwiggle", "Esmeralda Pumpernickel", "Horace Wobblewhiskers", "Jemima Floopernoodle", "Wilhelmina Gobbledygook", "Dudley Quirkypants", "Agatha Tinkleton", "Archibald McSquiggle", "Millicent Bunsenburner", "Mortimer Quirkysniff", "Tabitha Wifflebottom", "Percival Fuzzywiggle", "Gertrude Doodlepants", "Cornelius Bumblebop", "Prudence Biscuitfluff", "Egbert Puddingpants", "Hortense Quirkysnoot", "Winifred Wobbleflop", "Cedric Fluffernoodle", "Ethelbert Pipsqueak", "Percival McFiddlesticks", "Petunia Bumblegum", "Reginald Crumplehorn", "Mabel Quibblefuss", "Algernon Snorkelsnout", "Penelope Noodlebottom", "Beatrice Fuzzysnoot", "Cornelius McFlapdoodle", "Priscilla Crumpetwiggle", "Barnaby Quirkypants", "Esmeralda Tinkleton", "Horace McSquiggle", "Jemima Bunsenburner", "Wilhelmina Quirkysniff", "Dudley Wifflebottom", "Agatha Fuzzywiggle", "Archibald Doodlepants", "Millicent Bumblebop", "Mortimer Biscuitfluff", "Tabitha Puddingpants", "Percival Quirkysnoot", "Gertrude Wobbleflop", "Cornelius Fluffernoodle", "Prudence Pipsqueak", "Egbert McFiddlesticks", "Hortense Bumblegum", "Winifred Crumplehorn", "Cedric Quibblefuss", "Ethelbert Snorkelsnout", "Percival Noodlebottom", "Petunia Fuzzysnoot", "Reginald McFlapdoodle", "Mabel Crumpetwiggle", "Algernon Quirkypants", "Penelope Tinkleton", "Beatrice McSquiggle", "Cornelius Bunsenburner", "Priscilla Quirkysniff", "Barnaby Wifflebottom", "Esmeralda Fuzzywiggle", "Horace Doodlepants", "Jemima Bumblebop", "Wilhelmina Biscuitfluff", "Dudley Puddingpants", "Agatha Quirkysnoot", "Archibald Wobbleflop", "Millicent Fluffernoodle", "Mortimer Pipsqueak", "Tabitha McFiddlesticks", "Percival Bumblegum", "Gertrude Crumplehorn", "Cornelius Quibblefuss"};

    public TextMeshProUGUI textMeshPro;
    public Slider indexSlider;

    public bool nameSelected;

    private void Awake()
    {
        if (indexSlider != null)
        {
            indexSlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        int index = Mathf.Clamp(Mathf.RoundToInt(value), 0, 149);

        textMeshPro.text = names[index];

        if (!nameSelected) nameSelected = true;
    }
}
