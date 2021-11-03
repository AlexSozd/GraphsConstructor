# GraphsConstructor (English)
## Abstract
Program system Graphs (v. 1.0.3.1) was built for the Software Design Patterns coursework in 2017 and designed for visual graph constructing and manipulating.

The operations, realised in it, are:
- adding and deleting existing vertices,
- adding edges between existing vertices and deleting existing ones,
- the path calculation by one of the following algorithms: lexicographic breadth-first search, minimum degree, nested dissection, maximum cardinality search,
minimum fill-in,
- loading from and saving final graph in binary file.

## Design
### GUI (the window structure)

(In processing.)

### Code

(In processing.)

### Prospects

(In processing.)

# GraphsConstructor (Русский)
## Аннотация
Программная система Graphs (v. 1.0.3.1) была построена для курсового проекта по шаблонам проектирования в 2017 году и предназначена для визуального конструирования и
редактирования графов.

В системе реализованы следующие операции:
- добавление и удаление уже существующих вершин;
- добавление рёбер между существующими вершинами и удаление существующих;
- вычисление пути в графе по одному из следующих алгоритмов: лексикографический поиск в ширину (lexicographic breadth-first search), алгоритм минимальной степени, 
вложенного сечения (nested dissection), поиск максимальной кардинальности (maximum cardinality), алгоритм minimum fill-in;
- загрузка и сохранения получившегося графа в бинарном файле.

## Реализация
### Графический интерфейс программы (структура окна)

Большую часть окна занимает панель для изображения графа. Слева внизу и справа сбоку размещены кнопки и поля ввода номеров для редактирования - добавления и удаления
вершин и рёбер соответственно; для указания ребра следует вводить номера соединяемых вершин. Под ними - выпадающий список с перечнем алгоритмов построения пути и 
полем вывода результата; есть дополнительное поле для ввода номера для алгоритмов, требующих указания стартовой вершины.

Наведя курсор на вершину графа и нажимая левую клавишу мыши, можно перетащить её на новое место в области отрисовки.

### Код

В программной системе в виде класса (без наследования от базового) представлен простой граф (SimpleGraph), без весов и ориентации рёбер. В составе класса структура 
графа отображена списками (листами) вершин и ребёр, также описанные как классы; в числе методов - добавление и удаление вершины, добавление и удаление ребра,
вычисление степеней вершин, составление матриц смежности, расстояний и инцидентности.

Класс "вершина" (Vertex) содержит координаты центра отображения на панели окна, значение степени и порядкового номера в графе; в функции Draw задана отрисовка по 
умолчанию - в виде кружка радиусом 20 пикселей. В классе "ребро" (Edge) включены номера соединяемых вершин; поле *i* для хранения *меньшего* номера, *j* - *большего*, 
и задаются при инициализации экземпляра этого класса. В число компонентов также входят функции изменения и получения номера (меньшего или большего). Изображение рёбер
на экране определяется в функции рисования графа.

Для алгоритмов построения пути в графе был установлен единый интерфейс в виде абстрактного базового класса GraphCommand со ссылкой на объект "граф" и абстрактным
методом *Execute*, в производных классах содержащий реализацию одного из них (применение паттерна "Стратегия", *Strategy*). ... (В процессе написания)

### В перспективе

Добавить возможность добавлять/удалять и вершины, и рёбра с помощью мыши, сделав изменения обратимыми или повторимыми (через применение паттерна "Команда", 
*Command*).
