using Irminsul.Application.DTos.Characters;
using Irminsul.Application.Exceptions;
using Irminsul.Application.Interfaces;
using Irminsul.Application.Services;
using Irminsul.Domain.Entities;
using Irminsul.Domain.Enums;
using Moq;

namespace Irminsul.Tests;

public class CharacterServiceTests
{
    [Fact]
    public async Task GetCharacterByIdAsync_WhenCharacterDoesNotExist_ShouldThrowCharacterNotFoundException()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Character?)null);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var id = Guid.NewGuid();

        await Assert.ThrowsAsync<CharacterNotFoundException>(
            () => service.GetCharacterByIdAsync(id));
    }

    [Fact]
    public async Task GetCharacterByIdAsync_WhenCharacterExists_ShouldReturnCharacter()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        var character = new Character(
            "Hu Tao",
            "Fragrance in Thaw",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao.png",
            "Uma personagem de teste.",
            null);

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(character);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var id = Guid.NewGuid();

        var result = await service.GetCharacterByIdAsync(id);

        Assert.Same(character, result);
    }

    [Fact]
    public async Task UpdateCharacterAsync_WhenCharacterDoesNotExist_ShouldThrowCharacterNotFoundException()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Character?)null);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var id = Guid.NewGuid();

        var dto = new UpdateCharacterDto(
            "Hu Tao",
            "Fragrance in Thaw",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao.png",
            "Uma personagem de teste.",
            null);

        await Assert.ThrowsAsync<CharacterNotFoundException>(
            () => service.UpdateCharacterAsync(id, dto));
    }

    [Fact]
    public async Task UpdateCharacterAsync_WhenCharacterExists_ShouldUpdateCharacter()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        var character = new Character(
            "Hu Tao",
            "Fragrance in Thaw",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao.png",
            "Descrição antiga.",
            null);

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(character);

        repositoryMock
            .Setup(repository => repository.UpdateAsync(It.IsAny<Character>()))
            .ReturnsAsync((Character character) => character);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var id = character.Id;

        var dto = new UpdateCharacterDto(
            "Hu Tao Updated",
            "New Title",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao-updated.png",
            "Descrição nova.",
            null);

        var result = await service.UpdateCharacterAsync(id, dto);

        Assert.Same(character, result);
        Assert.Equal("Hu Tao Updated", result.Name);
        Assert.Equal("New Title", result.Title);
        Assert.Equal("Descrição nova.", result.Description);
    }

    [Fact]
    public async Task DeleteCharacterAsync_WhenCharacterDoesNotExist_ShouldThrowCharacterNotFoundException()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((Character?)null);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var id = Guid.NewGuid();

        await Assert.ThrowsAsync<CharacterNotFoundException>(
            () => service.DeleteCharacterAsync(id));
    }

    [Fact]
    public async Task DeleteCharacterAsync_WhenCharacterExists_ShouldDeleteCharacter()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        var character = new Character(
            "Hu Tao",
            "Fragrance in Thaw",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao.png",
            "Uma personagem de teste.",
            null);

        repositoryMock
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(character);

        repositoryMock
            .Setup(repository => repository.DeleteAsync(character.Id))
            .ReturnsAsync(character);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var result = await service.DeleteCharacterAsync(character.Id);

        Assert.Same(character, result);

        repositoryMock.Verify(
            repository => repository.DeleteAsync(character.Id),
            Times.Once);
    }

    [Fact]
    public async Task CreateCharacterAsync_ShouldCreateCharacter()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        repositoryMock
            .Setup(repository => repository.AddAsync(It.IsAny<Character>()))
            .ReturnsAsync((Character character) => character);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var dto = new CreateCharacterDto(
            "Hu Tao",
            "Fragrance in Thaw",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao.png",
            "Uma personagem de teste.",
            null);

        var result = await service.CreateCharacterAsync(dto);

        Assert.NotNull(result);
        Assert.Equal("Hu Tao", result.Name);
        Assert.Equal("Fragrance in Thaw", result.Title);
        Assert.Equal(CharacterRarity.FiveStars, result.Rarity);
        Assert.Equal(Vision.Pyro, result.Vision);
        Assert.Equal(WeaponType.Polearm, result.WeaponType);
        Assert.Equal(Nation.Liyue, result.Nation);
        Assert.Equal("https://example.com/hutao.png", result.ImageUrl);
        Assert.Equal("Uma personagem de teste.", result.Description);
        Assert.Null(result.Lore);
    }

    [Fact]
    public async Task CreateCharacterAsync_ShouldCallRepositoryAddAsyncOnce()
    {
        var repositoryMock = new Mock<ICharacterRepository>();

        repositoryMock
            .Setup(repository => repository.AddAsync(It.IsAny<Character>()))
            .ReturnsAsync((Character character) => character);

        var service = new CharacterService(
            repositoryMock.Object,
            null!);

        var dto = new CreateCharacterDto(
            "Hu Tao",
            "Fragrance in Thaw",
            CharacterRarity.FiveStars,
            Vision.Pyro,
            WeaponType.Polearm,
            Nation.Liyue,
            "https://example.com/hutao.png",
            "Uma personagem de teste.",
            null);

        await service.CreateCharacterAsync(dto);

        repositoryMock.Verify(
            repository => repository.AddAsync(It.IsAny<Character>()),
            Times.Once);
    }
}