using NUnit.Framework;
using System.Collections.Generic;
using static ExercicioTesteUnitario.Agenda;

namespace ExercicioTesteUnitario;

[TestFixture]
public class ContactServiceTests
{
    [Test]
    public void TestAddValidContact()
    {
        var service = new Agenda.ContactService();
        var name = "Dayanne Gomes";
        var phone = "(11) 99999-9999";
        var email = "dayanne@usp.br";

        var result = service.AddContact(name, phone, email);

        Assert.IsTrue(result);
        Assert.AreEqual(1, service.contacts.Count);

        var contact = service.contacts[0];
        Assert.AreEqual(name, contact.Name);
        Assert.AreEqual(phone, contact.Phone);
        Assert.AreEqual(email, contact.Email);
    }

    [Test]
    public void TestAddInvalidContact()
    {
        var service = new Agenda.ContactService();
        var name = "Dayanne Gomes";
        var phone = "(11) 99999-9999";
        var email = "dayanne";

        var result = service.AddContact(name, phone, email);

        Assert.IsFalse(result);
        Assert.AreEqual(0, service.contacts.Count);
    }

    public void TestAddDuplicateContact()
    {
        var contactService = new Agenda.ContactService();
        contactService.AddContact("Dayanne Gomes", "(11) 99999-9999", "dayanne@usp.br");

        bool result = contactService.AddContact("Dayanne Gomes", "(11) 99999-9999", "dayanne@usp.br");

        Assert.IsFalse(result);
        Assert.AreEqual(1, contactService.contacts.Count);
    }

    [Test]
    public void TestListContact()
    {
        var service = new Agenda.ContactService();
        service.AddContact("Maria", "(11) 99999-9999", "maria@usp.br");
        service.AddContact("João", "(11) 99999-9999", "joao@usp.br");
        var expected = "Lista de contatos:\n1. Maria - (11) 99999-9999 - maria@usp.br\n2. João - (11) 99999-9999 - joao@usp.br\n";

        var result = service.ListContacts();

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestListEmptyContact()
    {
        var service = new Agenda.ContactService();
        var expected = "Não há contatos cadastrados.";

        var result = service.ListContacts();

        Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestUpdateContact()
    {
        var contactService = new Agenda.ContactService();
        contactService.AddContact("Dayanne Gomes", "123456789", "dayanne@usp.br");

        string result = contactService.UpdateContact(0, "Dayanne Cristina", "987654321", "cristina@usp.br");

        Assert.AreEqual("Contato 'Dayanne Cristina' atualizado com sucesso.", result);
        Assert.AreEqual(1, contactService.contacts.Count);
        Assert.AreEqual("Dayanne Cristina", contactService.contacts[0].Name);
        Assert.AreEqual("987654321", contactService.contacts[0].Phone);
        Assert.AreEqual("cristina@usp.br", contactService.contacts[0].Email);
    }

    [Test]
    public void TestUpdateInvalidContact()
    {
        var contactService = new Agenda.ContactService();
        contactService.AddContact("Dayanne Gomes", "123456789", "dayanne@usp.br");

        string result = contactService.UpdateContact(1, "Dayanne Cristina", "987654321", "cristina@usp.br");

        Assert.AreEqual("Índice inválido. Tente novamente.", result);
        Assert.AreEqual(1, contactService.contacts.Count);
        Assert.AreEqual("Dayanne Cristina", contactService.contacts[0].Name);
        Assert.AreEqual("987654321", contactService.contacts[0].Phone);
        Assert.AreEqual("cristina@usp.br", contactService.contacts[0].Email);
    }

    [Test]
    public void TestRemoveContact()
    {
        var contactService = new Agenda.ContactService();
        contactService.AddContact("Dayanne", "123456", "dayanne@usp.br");

        string result = contactService.RemoveContact(0);

        Assert.AreEqual("Contato 'Dayanne' removido com sucesso.", result);
        Assert.AreEqual(0, contactService.contacts.Count);
    }

    [Test]
    public void TestRemoveInvalidContact()
    {
        var contactService = new Agenda.ContactService();
        contactService.AddContact("Dayanne", "123456", "dayanne@usp.br");

        string result = contactService.RemoveContact(1);

        Assert.AreEqual("Índice inválido. Tente novamente.", result);
        Assert.AreEqual(1, contactService.contacts.Count);
    }
}