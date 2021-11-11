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

The most area belongs to the panel for the graph image. Left below there is the *Save* (Сохранить) button for graph structure save in file and at right top there is 
the *Load* (Загрузить) button for loading graph from file. Right and beneath buttons and textboxes for entering numbers of adding or deleting edges and vertices 
respectively (the edge is defined by the numbers of connected by it vertices); under the last ones drop-down list for choosing of the path calculation algorithm and 
result output textbox are located; there is also an input textbox for start vertex number needed for some of algorithms at the beginning.

By cursor during the mouse left button click, you can transpose the vertex to new position at the panel.

### Code

We have the *SimpleGraph* class (without base class declaration) for the code definition of simple graph without weights or edge orientations. The graph structure 
represents by lists of vertices and edges, also defined as classes; it contains the following methods: adding and deleting vertex, adding and deleting edges, 
vertices’ degrees determination; adjacency, distance and incidence matrices calculation.

The *Vertex* class includes the image centre coordinates, vertex degree value and serial number in graph; the figure of vertex is defined as the circle with 20 pixels 
radius in the *Draw* method. The *Edge* class contains numbers of two connected by the edge vertices, the field *i* for the *smaller* and *j* – the *bigger* one, 
values are set in the object initialization process. It comprises the numbers’ change and set operations. The edges’ image is defined in the *Draw* method of the 
*SimpleGraph* class.

The system has the common interface for all path searching algorithms; it is the abstract base class *GraphCommand* consisted of pointer at graph object and abstract 
method *Execute* that is overridden to contain the algorithm implementation code in inherited classes (the *Strategy* design pattern use).

Objects of the *SimpleGraph* class initializes just during loading from file and before save in file or path algorithm execution (the *Lazy initialization* pattern) 
by the *Graphbuilder* class (the *Builder* pattern); the object of this class that contains formed graph is recorded in binary file. Operations of graph editing are 
executed at lists of vertices and edges. Result of path algorithm performance and start position number are encapsulated in the *Answer* and the *StartNumber* static 
classes respectively (the *Singleton* pattern).

### Prospects

Add the possibility to add/delete vertex or edge by the computer mouse, made changes reversible and repeatable (by the *Command* design pattern use).

Alter the classes’ structure, add base class for generalized graph and edge definition to determinate different graph types such as multigraph, oriented or directed 
graph (digraph). Make a decision about *GraphElementFactory* class usefulness – it’s determined but not used in main program (with previous item we can introduce 
*Abstract factory* and *Factory method* design patterns).

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

Большую часть окна занимает панель для изображения графа. Слева внизу помещена кнопка "Сохранить" - сохранение структуры графа в бинарном файле - и справа вверху
кнопка "Загрузить" - для загрузки графа из файла. Справа и ниже расположены кнопки и поля ввода номеров для редактирования - добавления и удаления рёбер и вершин 
соответственно (для указания ребра следует вводить номера соединяемых вершин); под последними - выпадающий список с перечнем алгоритмов построения пути и полем вывода 
результата; есть дополнительное поле для ввода номера для алгоритмов, требующих указания стартовой вершины.

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
методом *Execute*, в производных классах содержащий реализацию одного из них (применение паттерна "Стратегия", *Strategy*).

Экземпляры класса "граф" инициализируются в программе при сохранении или загрузки из файла и запуске расчёта пути по одному алгоритмов (Отложенная, или ленивая 
инициализация, *Lazy initialization*) через отдельный класс - GraphBuilder (паттерн "Строитель", *Builder*); в бинарном файле хранится содержащий созданный граф 
"билдер". При редактировании графа операции осуществляется над списками (листами) вершин и рёбер. Результат выполнения алгоритма поиска пути (Answer) и номер 
стартовой позиции (StartNumber) инкапсулированы в статических (static) классах (паттерн "Синглетон", *Singleton*).

### В перспективе

Добавить возможность добавлять/удалять и вершины, и рёбра с помощью мыши, сделав изменения обратимыми или повторимыми (через применение паттерна "Команда", 
*Command*).

Изменить структуру классов, добавив базовые классы для графа и рёбер с определением в производных других разновидностей - мультиграфа, взвешенного и ориентированного 
графа (оргграфа). Решить, нужен ли класс GraphElementFactory, сейчас он определён, но не используется в основной программе (вместе с предыдущим получаем полноценное 
применение паттернов "Абстрактная фабрика", *Abstract factory* и "Фабричный метод", *Factory method*).
