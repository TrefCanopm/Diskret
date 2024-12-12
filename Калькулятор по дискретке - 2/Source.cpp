#include <iostream>
#include <string>
#include <vector>
#include <map>
#include <stack>

using namespace std;

typedef vector<int> Vint;
typedef vector<Vint> VVint;

int* U = new int{ 2 };

int CheckingInput() 
{
	int number;
	bool numberCorrect = false;
	do
	{
		cin >> number;
		if (cin.fail())
		{
			cin.clear();
			cin.ignore(numeric_limits<streamsize>::max(), '\n');
			cout << "Введите корректное число" << endl;
			numberCorrect = true;
		}
		else
		{
			numberCorrect = false;
		}
	} while (numberCorrect);
	return number;
}

int CheckingInputNumberPositive()
{
	int number;
	bool numberCorrect = false;
	do
	{
		number = CheckingInput();
		if (number < 0)
		{
			numberCorrect = true;
			cout << "Введите число больше или равное 0" << endl;
		}
		else
		{
			numberCorrect = false;
		}
	} while (numberCorrect);
	return number;
}

bool CheckindMunberUniverse(int number)
{
	if ((number >= U[0]) && (number <= U[1]))
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

void ObevlenieUniverse()
{
	int n;
	cout << "Введите границы диапазона" << endl;
	cout << "Нижняя граница" << endl;
	U[0] = CheckingInput();
	cout << "Верхняя граница" << endl;
	U[1] = CheckingInput();

	if (U[0] > U[1])
	{
		int n = U[0];
		U[0] = U[1];
		U[1] = n;
	}
}

bool RepeatCheck(Vint set, int checkingNumber)
{
	for (int numberCheckingElement = 0; numberCheckingElement < set.size(); numberCheckingElement++)
	{
		if (set[numberCheckingElement] == checkingNumber)
		{
			return 0;
		}
	}
	return 1;
}
bool RepeatCheckMas(int* mas, int sizeMas, int checkingNumber)
{
	for (int numberCheckingElement = 0; numberCheckingElement < sizeMas; numberCheckingElement++)
	{
		if (mas[numberCheckingElement] == checkingNumber)
		{
			return 0;
		}
	}
	return 1;
}

Vint FillingSetRandom(Vint set, int size)
{
	int number;
	for (int i = 0; i < size; i++)
	{
		number = rand() % (U[1] - U[0] + 1) + U[0];
		if (i == 0)
		{
			set.push_back(number);
		}
		else
		{
			if (RepeatCheck(set, number))
			{
				set.push_back(number);
			}
			else
			{
				i--;
			}
		}
	}
	return set;
}

int BuildingSign()
{
	bool isChekTrue = false;

	cout << "Знак чисел множества" << endl;
	cout << "1) Положительные" << endl;
	cout << "2) Отрицательные" << endl;
	cout << "Выберете номер" << endl;

	do
		switch (CheckingInput())
		{
		case 1:
		{
			return 0;
		}
		case 2:
		{
			return 1;
		}
		default:
		{
			cout << "Введите номер из списка" << endl;
			isChekTrue = true;
			break;
		}
		}
	while (isChekTrue);
}
int BuildingEven()
{
	bool isChekTrue = false;

	cout << "Чётность множества" << endl;
	cout << "1) Чётные" << endl;
	cout << "2) Нечётные" << endl;
	cout << "Выберете номер" << endl;

	do
		switch (CheckingInput())
		{
		case 1:
		{
			return 0;
		}
		case 2:
		{
			return 1;
		}
		default:
		{
			cout << "Введите номер из списка" << endl;
			isChekTrue = false;
			break;
		}
		}
	while (isChekTrue);
}
void BuildingMultiplicity(int* arrow, int sizeArrow)
{
	int number;
	cout << "Введите числа которым будут кратны числа множества" << endl;
	for (int size = 0; size < sizeArrow; size++)
	{
		number = abs(CheckingInput());
		if (number != 0)
		{
			if (size == 0)
			{
				arrow[size] = number;
			}
			else
			{
				if (RepeatCheckMas (arrow, size, number))
				{
					arrow[size] = number;
				}
				else
				{
					size--;
				}
			}
		}
		else
		{
			size--;
			cout << "На ноль делить нельзя" << endl;
		}
	}
}

bool ChekingSign(int sign, int number)
{
	if (sign == 0)
	{
		if (number >= 0)
			return 1;
		else
			return 0;
	}
	else
	{
		if (number <= 0)
			return 1;
		else
			return 0;
	}
}
bool ChekingEven(int even, int number)
{
	if (even == 0)
	{
		if (number % 2 == 0)
			return 1;
		else
			return 0;
	}
	else
	{
		if (number % 2 == 1)
			return 1;
		else
			return 0;
	}
}
bool ChekingMultiplicity(int* multiplicity, int sizeMultiplicity, int number)
{
	for (int size = 0; size < sizeMultiplicity; size++)
	{
		if (number % multiplicity[size] != 0)
			return 0;
	}
	return 1;
}

Vint FillingSetCondition(Vint set, int size)
{
	bool isChek = true;

	int sign = -1;
	int even = -1;

	int* multiplicity = new int[0];
	int sizeMultiplicity = 0;

	while (isChek)
	{
		cout << "Условия по умолчанию - Знак: нет условия, Чётность: нет условия, Кратность: нет условия" << endl;
		cout << "Настройка условий" << endl;
		cout << "1) Знак чисел в множестве" << endl;
		cout << "2) Чётность чисел в множестве" << endl;
		cout << "3) Каким числам кратны числа из множества" << endl;
		cout << "4) Законьчить ввод условий" << endl;
		cout << "Введите номер необходимого действия" << endl;

		switch (CheckingInput())
		{
		case 1:
		{
			sign = BuildingSign();
			break;
		}
		case 2:
		{
			even = BuildingEven();
			break;
		}
		case 3:
		{
			delete[] multiplicity;
			cout << "Скольким числам должны кратны элементы" << endl;
			sizeMultiplicity = CheckingInputNumberPositive();
			multiplicity = new int[sizeMultiplicity];
			BuildingMultiplicity(multiplicity, sizeMultiplicity);
			break;
		}
		case 4:
		{
			isChek = false;
			break;
		}
		default:
		{
			cout << "Введите номер действия из списка" << endl;
			break;
		}
		}
	}

	int number;
	bool isSatisfiesCondition = true;
	for (int i = 0; i < size; i++)
	{
		number = rand() % (U[1] - U[0] + 1) + U[0];

		isSatisfiesCondition = RepeatCheck(set, number);

		if ((sign != -1) && isSatisfiesCondition)
		{
			isSatisfiesCondition = ChekingSign(sign, number);
		}
		if ((even != -1) && isSatisfiesCondition)
		{
			isSatisfiesCondition = ChekingEven(even, number);
		}
		if ((sizeMultiplicity != 0) && isSatisfiesCondition)
		{
			isSatisfiesCondition = ChekingMultiplicity(multiplicity, sizeMultiplicity, number);
		}
		if (isSatisfiesCondition)
		{
			set.push_back(number);
		}
		else
		{
			i--;
		}
	}

	return set;
}

Vint FillingSetInput(Vint set, int size)
{
	int number;
	for (int i = 0; i < size; i++)
	{
		number = CheckingInput();
		if (CheckindMunberUniverse(number))
		{
			if (i == 0)
			{
				set.push_back(number);
			}
			else
			{
				if (RepeatCheck(set, number))
				{
					set.push_back(number);
				}
				else
				{
					i--;
					cout << "Введённое число уже есть в множестве. Введите другое число" << endl;
				}
			}
		}
		else
		{
			cout << "Введёное число находится за пределами универсума" << endl;
			i--;
		}
	}
	return set;
}

Vint CompletionSet(string str)
{
	int size = 0;
	cout << str << endl;
	size = CheckingInput();

	Vint set;

	bool isCorrectNumber = true;

	cout << "Способы заполнения множества" << endl;
	cout << "1) Случайными числами из универсума" << endl;
	cout << "2) Числами с условием" << endl;
	cout << "3) С клавиатуры" << endl;
	cout << "Введите номер способа" << endl;
	do
	{
		switch (CheckingInputNumberPositive())
		{
		case 1:
		{
			set = FillingSetRandom(set, size);
			isCorrectNumber = false;
			break;
		}
		case 2:
		{
			set = FillingSetCondition(set, size);
			isCorrectNumber = false;
			break;
		}
		case 3:
		{
			set = FillingSetInput(set, size);
			isCorrectNumber = false;
			break;
		}
		default:
		{
			cout << "Введите номер из списка" << endl;
		}
		}
	} while (isCorrectNumber);

	return set;
}

void Print(Vint set, char name)
{
	cout << name << " ={" << set[0];
	for (int i = 1; i < set.size(); i++)
	{
		cout << ", " << set[i];
	}
	cout << '}' << endl;
}

bool ChekEnteringOperations(string str)
{
	bool isCorectEntering = true;

	bool isEnteringSet = false;
	bool isEnteringSign = false;
	bool isEnteringBracket = false;

	stack<char> bracket;

	for (int number = 0; number < str.size(); number++)
	{
		if ((str[number] == 'A') || (str[number] == 'B') || (str[number] == 'C'))
		{
			if (isEnteringSet)
			{
				isCorectEntering = false;
				break;
			}
			else
			{
				isEnteringSet = true;
				isEnteringSign = false;
				isEnteringBracket = false;
			}
		}

		if ((str[number] == '(') || (str[number] == '{') || (str[number] == '['))
		{
			if (isEnteringSet)
			{
				isCorectEntering = false;
				break;
			}
			else
			{
				bracket.push(str[number]);
                isEnteringBracket = true;
				isEnteringSign = false;
				isEnteringSet = false;
			}
		}

		if ((str[number] == ')') || (str[number] == '}') || (str[number] == ']'))
		{
			if (isEnteringSign)
			{
				isCorectEntering = false;
				break;
			}
			else
			{
				if (((bracket.top() == '(') && (str[number] == ')')) || ((bracket.top() == '{') && (str[number] == '}')) || ((bracket.top() == '[') && (str[number] == ']')))
				{
					isEnteringBracket = false;
					isEnteringSign = false;
					isEnteringSet = true;
					bracket.pop();
				}
				else
				{
					isCorectEntering = false;
					break;
				}
			}
		}

		if ((str[number] == '+') || (str[number] == '*') || (str[number] == '\\') || (str[number] == '^'))
		{
			if (isEnteringSign)
			{
				isCorectEntering = false;
				break;
			}
			else
			{
                isEnteringSign = true;
				isEnteringBracket = false;
				isEnteringSet = false;
			}
		}

		if (str[number] == '|')
		{
			if (isEnteringSet)
			{
				isCorectEntering = false;
				break;
			}
			else
			{
				isEnteringSign = true;
				isEnteringBracket = false;
				isEnteringSet = false;
			}
		}
	}

	return isCorectEntering;
}

string EnteringOperations()
{
	string str;

	bool isCorrectInput = true;
	bool isChek;
	while (isCorrectInput)
	{
		getline(cin >> ws, str);

		isChek = ChekEnteringOperations(str);

		if (isChek)
		{
			isCorrectInput = false;
		}
		else
		{
			cout << "Не правильный ввод" << endl;
		}
	}

	return str;
}

Vint IntersectionOperation(Vint A, Vint B)
{
	Vint set;

	for (int number = 0; number < A.size(); number++)
	{
		if (RepeatCheck(B, A[number]))
		{
			set.push_back(A[number]);
		}
	}

	for (int number = 0; number < B.size(); number++)
	{
		set.push_back(B[number]);
	}

	return set;
}

Vint UnificationOperation(Vint A, Vint B)
{
	Vint set;

	for (int number = 0; number < A.size(); number++)
	{
		if (!RepeatCheck(B, A[number]))
		{
			set.push_back(A[number]);
		}
	}

	return set;
}

Vint DifferenceOperation(Vint A, Vint B)
{
	Vint set;

	for (int number = 0; number < A.size(); number++)
	{
		if (RepeatCheck(B, A[number]))
		{
			set.push_back(A[number]);
		}
	}

	return set;
}

Vint DifferenceSymmetricOperation(Vint A, Vint B)
{
	Vint set;

	for (int number = 0; number < A.size(); number++)
	{
		if (RepeatCheck(B, A[number]))
		{
			set.push_back(A[number]);
		}
	}

	for (int number = 0; number < B.size(); number++)
	{
		if (RepeatCheck(A, B[number]))
		{
			set.push_back(B[number]);
		}
	}

	return set;
}

Vint AddOperation(Vint set)
{
	Vint temp;
	int count = 0;
	for (int number = U[0]; number <= U[1]; number++)
	{
		if (number == set[count])
		{
			count++;
		}
		else
		{
			temp.push_back(number);
		}
	}

	return temp;
}

Vint VariableHandling(Vint A, Vint B, stack<char>* signs)
{
	Vint set;
	while ((signs->top() == '|')&&(signs->size() != 0))
	{
		B = AddOperation(B);
		signs->pop();
	}

	if (signs->size() != 0)
	{
		switch (signs->top())
		{
		case '+':
		{
			set = IntersectionOperation(A, B);
			break;
		}
		case '*':
		{
			set = UnificationOperation(A, B);
			break;
		}
		case '\\':
		{
			set = DifferenceOperation(A, B);
			break;
		}
		case '^':
		{
			set = DifferenceSymmetricOperation(A, B);
			break;
		}
		}
	}

	signs->pop();

	Print(set, '100');

	return set;
}

void ActionMenu(Vint A, Vint B, Vint C)
{
	system("cls");
	
	Print(A, 'A');
	Print(B, 'B');
	Print(C, 'C');

	cout << "\tМеню операций" << endl;
	cout << " Пересечение - +" << endl;
	cout << " Объеденение - *" << endl;
	cout << " Разность - \\" << endl;
	cout << " Симетрическая разность - ^ " << endl;
	cout << " Дополнение - |" << endl;

	string str = EnteringOperations();

	bool isWasBracket = false;
	bool isBeginning = true;

	stack<Vint> operationsSets;
	stack<char> staples;
	stack<char> signs;

	Vint temp;
	Vint temp1;

	for (int number = 0; number < str.size(); number++)
	{
		switch (str[number])
		{
		case 'A':
		{
			if (isBeginning || isWasBracket)
			{
				isBeginning = false;
				isWasBracket = false;
				if (signs.size() != 0)
				{
					if (signs.top() == '|')
					{
						operationsSets.push(AddOperation(A));
					}
					else
					{
						operationsSets.push(A);
					}
				}
				else
				{
					operationsSets.push(A);
				}
			}
			else
			{
				temp = operationsSets.top();
				operationsSets.pop();
				operationsSets.push(VariableHandling(temp, A, &signs));
			}
			break;
		}
		case 'B':
		{
			if (isBeginning || isWasBracket)
			{
				isBeginning = false;
				isWasBracket = false;
				if (signs.size() != 0)
				{
					if (signs.top() == '|')
					{
						operationsSets.push(AddOperation(B));
					}
					else
					{
						operationsSets.push(B);
					}
				}
				else
				{
					operationsSets.push(B);
				}
			}
			else
			{
				temp = operationsSets.top();
				operationsSets.pop();
				operationsSets.push(VariableHandling(temp, B, &signs));
			}
			break;
		}
		case 'C':
		{
			if (isBeginning || isWasBracket)
			{
				isBeginning = false;
				isWasBracket = false;
				if (signs.size() != 0)
				{
					if (signs.top() == '|')
					{
						operationsSets.push(AddOperation(C));
					}
					else
					{
						operationsSets.push(C);
					}
				}
				else
				{
					operationsSets.push(C);
				}
			}
			else
			{
				temp = operationsSets.top();
				operationsSets.pop();
				operationsSets.push(VariableHandling(temp, C, &signs));
			}
			break;
		}

		case '(':
		{
			isWasBracket = true;
			staples.push('(');
			break;
		}
		case '{':
		{
			isWasBracket = true;
			staples.push('{');
			break;
		}
		case '[':
		{
			isWasBracket = true;
			staples.push('[');
			break;
		}

		case ')':
		{
			if (operationsSets.size() != 1)
			{
				temp = operationsSets.top();
				operationsSets.pop();

				temp1 = operationsSets.top();
				operationsSets.pop();

				operationsSets.push(VariableHandling(temp1, temp, &signs));
			}
			break;
		}
		case '}':
		{
			if (operationsSets.size() != 1)
			{
				temp = operationsSets.top();
				operationsSets.pop();

				temp1 = operationsSets.top();
				operationsSets.pop();

				operationsSets.push(VariableHandling(temp1, temp, &signs));
			}
			break;
		}
		case ']':
		{
			if (operationsSets.size() != 1)
			{
				temp = operationsSets.top();
				operationsSets.pop();

				temp1 = operationsSets.top();
				operationsSets.pop();

				operationsSets.push(VariableHandling(temp1, temp, &signs));
			}
			break;
		}

		case '+':
		{
			signs.push('+');
			break;
		}
		case '*':
		{
			signs.push('*');
			break;
		}
		case '\\':
		{
			signs.push('\\');
			break;
		}
		case '^':
		{
			signs.push('^');
			break;
		}
		case '|':
		{
			signs.push('|');
			break;
		}
		}
	}
	
	Print(operationsSets.top(), 'L');
}

int main()
{
	setlocale(LC_ALL, "RUS");

	ObevlenieUniverse();

	Vint A;
	Vint B;
	Vint C;

	A = CompletionSet("Введите мощность множество А");
	Print(A, 'A');

	B = CompletionSet("Введите мощность множество B");
	Print(B, 'B');

	C = CompletionSet("Введите мощность множество C");
	Print(C, 'C');
	// перенести остатки проекта

	ActionMenu(A, B, C);
}