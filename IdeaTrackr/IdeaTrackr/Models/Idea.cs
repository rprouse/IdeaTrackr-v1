using System;

namespace IdeaTrackr.Models
{
    public class Idea : BaseNotifyPropertyChanged
    {
        string _id;
        string _name;
        string _problem;
        string _solution;
        string _notes;
        int _rating;
        DateTimeOffset? _createdAt;
        DateTimeOffset? _updatedAt;

        public string Id
        {
            get { return _id; }
            set
            {
                if(_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Problem
        {
            get { return _problem; }
            set
            {
                if (_problem != value)
                {
                    _problem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Solution
        {
            get { return _solution; }
            set
            {
                if (_solution != value)
                {
                    _solution = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Notes
        {
            get { return _notes; }
            set
            {
                if (_notes != value)
                {
                    _notes = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTimeOffset? CreatedAt
        {
            get { return _createdAt; }
            set
            {
                if (_createdAt != value)
                {
                    _createdAt = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public DateTimeOffset? UpdatedAt
        {
            get { return _updatedAt; }
            set
            {
                if (_updatedAt != value)
                {
                    _updatedAt = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
