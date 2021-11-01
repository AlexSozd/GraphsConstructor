# GraphsConstructor (English)
## Abstract
Program system Graphs (v. 1.0.3.1) was built for the Software Design Patterns coursework in 2017 and designed for visual graph constructing and manipulating.

The operations, realised in it, are:
- adding and deleting existing vertices,
- adding edges between existing vertices and deleting existing ones,
- the path calculation by one of the following algorithms: lexicographic breadth-first search, minimum degree, nested dissection, maximum cardinality search,
minimum fill-in,
- loading from and saving final graph in binary file.

## Realization

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
### Код
В программной системе в виде класса (без наследования от базового) представлен простой граф (SimpleGraph). В составе класса структура графа отображена списками (листами) 
вершин и ребёр, также описанные как классы. Класс "вершина" (Vertex) содержит координаты центра отображения на панели окна, значение степени и порядкового номера в 
графе; в функции Draw задана отрисовка по умолчанию - в виде кружка радиусом 20 пикселей. ... (В процессе написания)
