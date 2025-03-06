На все объекты на сцене добавлены коллайдеры:
- слой Stand, если горизонтальная поверхность
- слой Default для вертикальной части объекта под горизонтальной поверхностью
- слой Drag для объекта, который можно захватить

Предполагается, что Stand-коллайдеры не должны пересекаться, кроме случая их пересечения с полом.
В скрипте DragAndDropController есть поле для коллайдера пола.

При отпускании текущего захваченного объекта запускается проверка точки, в которую от встанет.

На захватываемых объектах обозначена нижняя точка, и прикреаплён скрипт DraggingComponent. Проверка позиции установки производится в методе ExploreDown в DraggingComponent. Для этого от нижней точки объекта производится RaycastAll.
Выбираются объекты из слоя Stand и для каждого определяются коллайдеры, находящиеся в точке пересечения с лучом. Точка не проходит проверку если:
- это не нижняя точка объекта и на ней помимо Stand-коллайдера есть другие
- это нижняя точка объекта и в ней есть один другой коллайдер, который является полом

Если не удалось найти точку, деалется обратный RaycastAll и нужной считается вторая точка из этого raycast. Предполагается, что это всегда будет точка пересечения нижней части не Stand-объекта с полом.
