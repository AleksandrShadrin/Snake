namespace Snake.Presentation.Selectable
{
    internal interface ISelectable
    {
        ///<summary>
        /// Выбирает данный объект
        /// </summary>
        void Select();

        ///<summary>
        /// Отменяет выбор данного объекта
        /// </summary>
        void UnSelect();
    }
}
