//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Resources;
//using intSoft.Res.Messages;

//namespace IntSoft.DAL.ExceptionHandling
//{
//    public class SqlExceptionHandler
//    {
//        private readonly Dictionary<string, string> _constraintNameErrorMessageDictionary =
//            new Dictionary<string, string>();

//        private readonly Dictionary<SqlExceptionErrorCode, string> _sqlExceptionErrorCodeErrorMessageDictionary =
//            new Dictionary<SqlExceptionErrorCode, string>();

//        public SqlExceptionHandler()
//        {
//            _sqlExceptionErrorCodeErrorMessageDictionary.Add(SqlExceptionErrorCode.SqlReferenceConstraintViolation,
//                Messages.UsedEntityCantBeDeleted);
//            _sqlExceptionErrorCodeErrorMessageDictionary.Add(SqlExceptionErrorCode.SqlUniqueConstraintViolation,
//                Messages.UniqueConstraintViolation);
//            _sqlExceptionErrorCodeErrorMessageDictionary.Add(SqlExceptionErrorCode.SqlUniqueIndexViolation,
//                Messages.UniqueIndexViolation);
//        }

//        public Type MessagesResourceType { get; set; }

//        public bool TryHandleException(Exception exception, ref string errorMessage)
//        {
//            if (exception == null) return false;

//            if (TryHandleReferenceConstraintViolation(exception, ref errorMessage)) return true;

//            if (TryHandleUniqueConstraintViolation(exception, ref errorMessage)) return true;

//            return TryHandleUniqueIndexViolation(exception, ref errorMessage);
//        }
        
//        /// <summary>
//        /// This method will define a map between the name of the Sql constraint and a message that will be sent to the user 
//        /// when a viloation occured to this constraint.
//        /// </summary>
//        /// <param name="constraintName">
//        /// The name of the constraint in the database.
//        /// </param>
//        /// <param name="errorMessage">
//        /// A user-friendly error message corresponding to the constraint violation.
//        /// </param>
//        public void AddConstraintNameErrorMessage(string constraintName, string errorMessage)
//        {
//            if (MessagesResourceType == null)
//            {
//                throw new Exception("MessagesResourceType us not set!");
//            }
//            if (!_constraintNameErrorMessageDictionary.ContainsKey(constraintName))
//                _constraintNameErrorMessageDictionary.Add(constraintName, errorMessage);
//        }

//        private bool TryHandleUniqueConstraintViolation(Exception ex, ref string errorMessage)
//        {
//            return TryHandleException(ex, ref errorMessage, SqlExceptionErrorCode.SqlUniqueConstraintViolation);
//        }

//        private bool TryHandleUniqueIndexViolation(Exception ex, ref string errorMessage)
//        {
//            return TryHandleException(ex, ref errorMessage, SqlExceptionErrorCode.SqlUniqueIndexViolation);
//        }

//        private bool TryHandleReferenceConstraintViolation(Exception ex, ref string errorMessage)
//        {
//            return TryHandleException(ex, ref errorMessage, SqlExceptionErrorCode.SqlReferenceConstraintViolation);
//        }

//        private bool TryHandleException(Exception ex, ref string errorMessage,
//            SqlExceptionErrorCode sqlExceptionErrorCode)
//        {
//            var sqlEx = GetSqlExceptionIfExist(ex);

//            if (sqlEx == null || sqlEx.Number != (int) sqlExceptionErrorCode)
//                return false;

//            if (TryGetErrorMessage(sqlEx, ref errorMessage))
//                return true;

//            errorMessage = _sqlExceptionErrorCodeErrorMessageDictionary.ContainsKey(sqlExceptionErrorCode)
//                ? _sqlExceptionErrorCodeErrorMessageDictionary[sqlExceptionErrorCode]
//                : sqlExceptionErrorCode.ToString();
//            return true;
//        }

//        private SqlException GetSqlExceptionIfExist(Exception ex)
//        {
//            var innerException = ex;

//            while (innerException.InnerException != null)
//            {
//                innerException = innerException.InnerException;
//            }

//            return innerException as SqlException;
//        }

//        private bool TryGetErrorMessage(SqlException sqlException, ref string errorMessage)
//        {
//            foreach (var pair in _constraintNameErrorMessageDictionary)
//            {
//                if (sqlException.Message.Contains(pair.Key))
//                {
//                    errorMessage = ResourceHelper. pair.Value;
//                    return true;
//                }
//            }

//            return false;
//        }
//    }
//}